/*!
 * ECharts, a javascript interactive chart library.
 *
 * Copyright (c) 2015, Baidu Inc.
 * All rights reserved.
 *
 * LICENSE
 * https://github.com/ecomfe/echarts/blob/master/LICENSE.txt
 */

/**
 * echarts
 *
 * @desc echartsThư viện biểu đồ Javascript thuần túy dựa trên Canvas, cung cấp biểu đồ thống kê dữ liệu trực quan, sinh động, có thể tương tác và tùy chỉnh theo cá nhân.
 * @author Kener (@Kener-林峰, kener.linfeng@gmail.com)
 *
 */
define(function (require) {
    var ecConfig = require('./config');
    var zrUtil = require('zrender/tool/util');
    var zrEvent = require('zrender/tool/event');

    var self = {};

    var _canvasSupported = require('zrender/tool/env').canvasSupported;
    var _idBase = new Date() - 0;
    var _instances = {};    // EChartschỉ mục map của thực thể
    var DOM_ATTRIBUTE_KEY = '_echarts_instance_';

    self.version = '2.2.7';
    self.dependencies = {
        zrender: '2.1.1'
    };
    /**
     * Phương thức cổng vào
     */
    self.init = function (dom, theme) {
        var zrender = require('zrender');
        if ((zrender.version.replace('.', '') - 0) < (self.dependencies.zrender.replace('.', '') - 0)) {
            console.error(
                'ZRender ' + zrender.version
                + ' is too old for ECharts ' + self.version
                + '. Current version need ZRender '
                + self.dependencies.zrender + '+'
            );
        }

        dom = dom instanceof Array ? dom[0] : dom;

        // Chỉ mục ánh xạ giữa dom và thực thể echarts
        var key = dom.getAttribute(DOM_ATTRIBUTE_KEY);
        if (!key) {
            key = _idBase++;
            dom.setAttribute(DOM_ATTRIBUTE_KEY, key);
        }

        if (_instances[key]) {
            // Khởi tạo nhiều lần trên cùng một dom sẽ tự động giải phóng thực thể đã có
            _instances[key].dispose();
        }
        _instances[key] = new Echarts(dom);
        _instances[key].id = key;
        _instances[key].canvasSupported = _canvasSupported;
        _instances[key].setTheme(theme);

        return _instances[key];
    };

    /**
     * Lấy thực thể ECharts qua id, id có thể đọc được sau khi khởi tạo
     */
    self.getInstanceById = function (key) {
        return _instances[key];
    };

    /**
     * Trung tâm thông báo
     */
    function MessageCenter() {
        zrEvent.Dispatcher.call(this);
    }
    zrUtil.merge(MessageCenter.prototype, zrEvent.Dispatcher.prototype, true);

    /**
     * Lớp giao diện Echarts được triển khai dựa trên zrender
     * @param {HtmlElement} dom Bắt buộc
     */
    function Echarts(dom) {
        // Fxxk IE11 for breaking initialization without a warrant;
        // Just set something to let it be!
        // by kener 2015-01-09
        dom.innerHTML = '';
        this._themeConfig = {}; // zrUtil.clone(ecConfig);

        this.dom = dom;
        // this._zr;
        // this._option;                    // curOption clone
        // this._optionRestore;             // for restore;
        // this._island;
        // this._toolbox;
        // this._timeline;
        // this._refreshInside;             // Cờ đánh dấu làm mới nội bộ

        this._connected = false;
        this._status = {                    // Dùng để giao tiếp giữa các biểu đồ
            dragIn: false,
            dragOut: false,
            needRefresh: false
        };
        this._curEventType = false;         // Đèn tín hiệu phá vòng lặp
        this._chartList = [];               // Thực thể biểu đồ

        this._messageCenter = new MessageCenter();

        this._messageCenterOutSide = new MessageCenter();    // Trung tâm thông báo bên ngoài của lớp Echarts, làm nhiệm vụ chuyển tiếp thông báo của lớp Echarts

        // Phương thức resize thường được gắn vào window.resize, closure một this
        this.resize = this.resize();

        // Khởi tạo::hàm tạo
        this._init();
    }

    /**
     * ZRender EVENT
     *
     * @inner
     * @const
     * @type {Object}
     */
    var ZR_EVENT = require('zrender/config').EVENT;

    /**
     * cần gắn lắng nghezrenderDanh sách sự kiện
     *
     * @const
     * @inner
     * @type {Array}
     */
    var ZR_EVENT_LISTENS = [
        'CLICK', 'DBLCLICK', 'MOUSEOVER', 'MOUSEOUT',
        'DRAGSTART', 'DRAGEND', 'DRAGENTER', 'DRAGOVER', 'DRAGLEAVE', 'DROP'
    ];

    /**
     * đối với thực thể echarts, thuộc tính chartList, gọi lần lượt từng phương thức, thứ tự duyệt là ngược
     * Vì logic giống nhau được dùng nhiều lần trong quá trình xử lý hành vi mặc định khi sự kiện kích hoạt, nên phương thức này được trừu tượng hóa
     * Vì trong mọi trường hợp gọi, tối đa chỉ có hai tham số, nên xét về hiệu năng và kích thước, ở đây không dùng call hay apply nữa
     *
     * @inner
     * @param {ECharts} ecInstance EChartsthực thể
     * @param {string} methodName Tên phương thức cần gọi
     * @param {*} arg0 Tham số gọi1
     * @param {*} arg1 Tham số gọi2
     * @param {*} arg2 Tham số gọi3
     */
    function callChartListMethodReverse(ecInstance, methodName, arg0, arg1, arg2) {
        var chartList = ecInstance._chartList;
        var len = chartList.length;

        while (len--) {
            var chart = chartList[len];
            if (typeof chart[methodName] === 'function') {
                chart[methodName](arg0, arg1, arg2);
            }
        }
    }

    Echarts.prototype = {
        /**
         * Khởi tạo::hàm tạo
         */
        _init: function () {
            var self = this;
            var _zr = require('zrender').init(this.dom);
            this._zr = _zr;

            // wrap: n,e,d,t for name event data this
            this._messageCenter.dispatch = function(type, event, eventPackage, that) {
                eventPackage = eventPackage || {};
                eventPackage.type = type;
                eventPackage.event = event;

                self._messageCenter.dispatchWithContext(type, eventPackage, that);
                self._messageCenterOutSide.dispatchWithContext(type, eventPackage, that);

                // Đoạn mã đã chú thích dưới đây,@see: https://github.com/ecomfe/echarts-discuss/issues/3
                // if (type != 'HOVER' && type != 'MOUSEOUT') {    // Sự kiện thường xuyên được ném ra trực tiếp
                //     setTimeout(function(){
                //         self._messageCenterOutSide.dispatchWithContext(
                //             type, eventPackage, that
                //         );
                //     },50);
                // }
                // else {
                //     self._messageCenterOutSide.dispatchWithContext(
                //         type, eventPackage, that
                //     );
                // }
            };

            this._onevent = function(param){
                return self.__onevent(param);
            };
            for (var e in ecConfig.EVENT) {
                if (e != 'CLICK' && e != 'DBLCLICK'
                    && e != 'HOVER' && e != 'MOUSEOUT' && e != 'MAP_ROAM'
                ) {
                    this._messageCenter.bind(ecConfig.EVENT[e], this._onevent, this);
                }
            }


            var eventBehaviors = {};
            this._onzrevent = function (param) {
                return self[eventBehaviors[ param.type ]](param);
            };

            // Gắn các sự kiện được quan tâm
            for (var i = 0, len = ZR_EVENT_LISTENS.length; i < len; i++) {
                var eventName = ZR_EVENT_LISTENS[i];
                var eventValue = ZR_EVENT[eventName];
                eventBehaviors[eventValue] = '_on' + eventName.toLowerCase();
                _zr.on(eventValue, this._onzrevent);
            }

            this.chart = {};            // Chỉ mục biểu đồ
            this.component = {};        // Chỉ mục thành phần

            // Biểu đồ tích hợp sẵn
            // Đảo
            var Island = require('./chart/island');
            this._island = new Island(this._themeConfig, this._messageCenter, _zr, {}, this);
            this.chart.island = this._island;

            // Thành phần chung tích hợp sẵn
            // Hộp công cụ
            var Toolbox = require('./component/toolbox');
            this._toolbox = new Toolbox(this._themeConfig, this._messageCenter, _zr, {}, this);
            this.component.toolbox = this._toolbox;

            var componentLibrary = require('./component');
            componentLibrary.define('title', require('./component/title'));
            componentLibrary.define('tooltip', require('./component/tooltip'));
            componentLibrary.define('legend', require('./component/legend'));

            if (_zr.getWidth() === 0 || _zr.getHeight() === 0) {
                console.error('Dom’s width & height should be ready before init.');
            }
        },

        /**
         * EChartsTrung tâm xử lý sự kiện
         */
        __onevent: function (param){
            param.__echartsId = param.__echartsId || this.id;

            // Sự kiện từ biểu đồ liên động khác
            var fromMyself = (param.__echartsId === this.id);

            if (!this._curEventType) {
                this._curEventType = param.type;
            }

            switch (param.type) {
                case ecConfig.EVENT.LEGEND_SELECTED :
                    this._onlegendSelected(param);
                    break;
                case ecConfig.EVENT.DATA_ZOOM :
                    if (!fromMyself) {
                        var dz = this.component.dataZoom;
                        if (dz) {
                            dz.silence(true);
                            dz.absoluteZoom(param.zoom);
                            dz.silence(false);
                        }
                    }
                    this._ondataZoom(param);
                    break;
                case ecConfig.EVENT.DATA_RANGE :
                    fromMyself && this._ondataRange(param);
                    break;
                case ecConfig.EVENT.MAGIC_TYPE_CHANGED :
                    if (!fromMyself) {
                        var tb = this.component.toolbox;
                        if (tb) {
                            tb.silence(true);
                            tb.setMagicType(param.magicType);
                            tb.silence(false);
                        }
                    }
                    this._onmagicTypeChanged(param);
                    break;
                case ecConfig.EVENT.DATA_VIEW_CHANGED :
                    fromMyself && this._ondataViewChanged(param);
                    break;
                case ecConfig.EVENT.TOOLTIP_HOVER :
                    fromMyself && this._tooltipHover(param);
                    break;
                case ecConfig.EVENT.RESTORE :
                    this._onrestore();
                    break;
                case ecConfig.EVENT.REFRESH :
                    fromMyself && this._onrefresh(param);
                    break;
                // Đồng bộ chuột
                case ecConfig.EVENT.TOOLTIP_IN_GRID :
                case ecConfig.EVENT.TOOLTIP_OUT_GRID :
                    if (!fromMyself) {
                        // Chỉ xử lý đồng bộ chuột từ bên ngoài
                        var grid = this.component.grid;
                        if (grid) {
                            this._zr.trigger(
                                'mousemove',
                                {
                                    connectTrigger: true,
                                    zrenderX: grid.getX() + param.x * grid.getWidth(),
                                    zrenderY: grid.getY() + param.y * grid.getHeight()
                                }
                            );
                        }
                    }
                    else if (this._connected) {
                        // Đến từ chính mình, và khi có liên động nhiều biểu đồ, phân phối tham số sửa đổi ánh xạ tọa độ không gian
                        var grid = this.component.grid;
                        if (grid) {
                            param.x = (param.event.zrenderX - grid.getX()) / grid.getWidth();
                            param.y = (param.event.zrenderY - grid.getY()) / grid.getHeight();
                        }
                    }
                    break;
                /*
                case ecConfig.EVENT.RESIZE :
                case ecConfig.EVENT.DATA_CHANGED :
                case ecConfig.EVENT.PIE_SELECTED :
                case ecConfig.EVENT.MAP_SELECTED :
                    break;
                */
            }

            // Liên động nhiều biểu đồ, chỉ phân phối sự kiện cấp một của chính mình, tránh vòng lặp sự kiện dây chuyền
            if (this._connected && fromMyself && this._curEventType === param.type) {
                for (var c in this._connected) {
                    this._connected[c].connectedEventHandler(param);
                }
                // Đặt lại sau khi phân phối xong
                this._curEventType = null;
            }

            if (!fromMyself || (!this._connected && fromMyself)) {  // Đặt lại sau khi xử lý xong sự kiện liên động
                this._curEventType = null;
            }
        },

        /**
         * Sự kiện nhấn chuột, phản hồi sự kiện zrender, đóng gói rồi phân phối đến lớp Echarts
         */
        _onclick: function (param) {
            callChartListMethodReverse(this, 'onclick', param);

            if (param.target) {
                var ecData = this._eventPackage(param.target);
                if (ecData && ecData.seriesIndex != null) {
                    this._messageCenter.dispatch(
                        ecConfig.EVENT.CLICK,
                        param.event,
                        ecData,
                        this
                    );
                }
            }
        },

        /**
         * Sự kiện nhấn đúp, phản hồi sự kiện zrender, đóng gói rồi phân phối đến lớp Echarts
         */
        _ondblclick: function (param) {
            callChartListMethodReverse(this, 'ondblclick', param);

            if (param.target) {
                var ecData = this._eventPackage(param.target);
                if (ecData && ecData.seriesIndex != null) {
                    this._messageCenter.dispatch(
                        ecConfig.EVENT.DBLCLICK,
                        param.event,
                        ecData,
                        this
                    );
                }
            }
        },

        /**
         * Sự kiện di chuột vào, phản hồi sự kiện zrender, đóng gói rồi phân phối đến lớp Echarts
         */
        _onmouseover: function (param) {
            if (param.target) {
                var ecData = this._eventPackage(param.target);
                if (ecData && ecData.seriesIndex != null) {
                    this._messageCenter.dispatch(
                        ecConfig.EVENT.HOVER,
                        param.event,
                        ecData,
                        this
                    );
                }
            }
        },

        /**
         * Sự kiện di chuột ra, phản hồi sự kiện zrender, đóng gói rồi phân phối đến lớp Echarts
         */
        _onmouseout: function (param) {
            if (param.target) {
                var ecData = this._eventPackage(param.target);
                if (ecData && ecData.seriesIndex != null) {
                    this._messageCenter.dispatch(
                        ecConfig.EVENT.MOUSEOUT,
                        param.event,
                        ecData,
                        this
                    );
                }
            }
        },

        /**
         * dragstartHàm callback, triển khai tính năng có thể tính toán
         */
        _ondragstart: function (param) {
            // Đặt lại cờ kéo-thả dùng cho giao tiếp giữa các biểu đồ
            this._status = {
                dragIn: false,
                dragOut: false,
                needRefresh: false
            };

            callChartListMethodReverse(this, 'ondragstart', param);
        },

        /**
         * draggingHàm callback, triển khai tính năng có thể tính toán
         */
        _ondragenter: function (param) {
            callChartListMethodReverse(this, 'ondragenter', param);
        },

        /**
         * dragstartHàm callback, triển khai tính năng có thể tính toán
         */
        _ondragover: function (param) {
            callChartListMethodReverse(this, 'ondragover', param);
        },

        /**
         * dragstartHàm callback, triển khai tính năng có thể tính toán
         */
        _ondragleave: function (param) {
            callChartListMethodReverse(this, 'ondragleave', param);
        },

        /**
         * dragstartHàm callback, triển khai tính năng có thể tính toán
         */
        _ondrop: function (param) {
            callChartListMethodReverse(this, 'ondrop', param, this._status);
            this._island.ondrop(param, this._status);
        },

        /**
         * dragdone hàm callback, triển khai tính năng có thể tính toán
         */
        _ondragend: function (param) {
            callChartListMethodReverse(this, 'ondragend', param, this._status);

            this._timeline && this._timeline.ondragend(param, this._status);
            this._island.ondragend(param, this._status);

            // Đã xảy ra tính toán lại
            if (this._status.needRefresh) {
                this._syncBackupData(this._option);

                var messageCenter = this._messageCenter;
                messageCenter.dispatch(
                    ecConfig.EVENT.DATA_CHANGED,
                    param.event,
                    this._eventPackage(param.target),
                    this
                );
                messageCenter.dispatch(ecConfig.EVENT.REFRESH, null, null, this);
            }
        },

        /**
         * Phản hồi khi chọn chú giải
         */
        _onlegendSelected: function (param) {
            // Dùng để giao tiếp giữa các biểu đồ
            this._status.needRefresh = false;
            callChartListMethodReverse(this, 'onlegendSelected', param, this._status);

            if (this._status.needRefresh) {
                this._messageCenter.dispatch(ecConfig.EVENT.REFRESH, null, null, this);
            }
        },

        /**
         * Phản hồi thu phóng vùng dữ liệu
         */
        _ondataZoom: function (param) {
            // Dùng để giao tiếp giữa các biểu đồ
            this._status.needRefresh = false;
            callChartListMethodReverse(this, 'ondataZoom', param, this._status);

            if (this._status.needRefresh) {
                this._messageCenter.dispatch(ecConfig.EVENT.REFRESH, null, null, this);
            }
        },

        /**
         * Phản hồi di chuyển trong miền giá trị
         */
        _ondataRange: function (param) {
            this._clearEffect();
            // Dùng để giao tiếp giữa các biểu đồ
            this._status.needRefresh = false;
            callChartListMethodReverse(this, 'ondataRange', param, this._status);

            // Không ảnh hưởng lẫn nhau, chỉ cần làm mới trực tiếp
            if (this._status.needRefresh) {
                this._zr.refreshNextFrame();
            }
        },

        /**
         * Phản hồi chuyển đổi kiểu động
         */
        _onmagicTypeChanged: function () {
            this._clearEffect();
            this._render(this._toolbox.getMagicOption());
        },

        /**
         * Phản hồi sửa đổi chế độ xem dữ liệu
         */
        _ondataViewChanged: function (param) {
            this._syncBackupData(param.option);
            this._messageCenter.dispatch(
                ecConfig.EVENT.DATA_CHANGED,
                null,
                param,
                this
            );
            this._messageCenter.dispatch(ecConfig.EVENT.REFRESH, null, null, this);
        },

        /**
         * tooltipgiao tiếp với biểu đồ
         */
        _tooltipHover: function (param) {
            var tipShape = [];
            callChartListMethodReverse(this, 'ontooltipHover', param, tipShape);
        },

        /**
         * Khôi phục
         */
        _onrestore: function () {
            this.restore();
        },

        /**
         * Làm mới
         */
        _onrefresh: function (param) {
            this._refreshInside = true;
            this.refresh(param);
            this._refreshInside = false;
        },

        /**
         * Đồng bộ ngược sau khi sửa đổi dữ liệudataZoomdữ liệu sao lưu đang giữ
         */
        _syncBackupData: function (curOption) {
            this.component.dataZoom && this.component.dataZoom.syncBackupData(curOption);
        },

        /**
         * Đóng góitệp đính kèm sự kiện của lớp Echarts
         */
        _eventPackage: function (target) {
            if (target) {
                var ecData = require('./util/ecData');

                var seriesIndex = ecData.get(target, 'seriesIndex');
                var dataIndex = ecData.get(target, 'dataIndex');

                dataIndex = seriesIndex != -1 && this.component.dataZoom
                            ? this.component.dataZoom.getRealDataIndex(
                                seriesIndex,
                                dataIndex
                              )
                            : dataIndex;
                return {
                    seriesIndex: seriesIndex,
                    seriesName: (ecData.get(target, 'series') || {}).name,
                    dataIndex: dataIndex,
                    data: ecData.get(target, 'data'),
                    name: ecData.get(target, 'name'),
                    value: ecData.get(target, 'value'),
                    special: ecData.get(target, 'special')
                };
            }
            return;
        },

        _noDataCheck: function(magicOption) {
            var series = magicOption.series;

            for (var i = 0, l = series.length; i < l; i++) {
                if (series[i].type == ecConfig.CHART_TYPE_MAP
                    || (series[i].data && series[i].data.length > 0)
                    || (series[i].markPoint && series[i].markPoint.data && series[i].markPoint.data.length > 0)
                    || (series[i].markLine && series[i].markLine.data && series[i].markLine.data.length > 0)
                    || (series[i].nodes && series[i].nodes.length > 0)
                    || (series[i].links && series[i].links.length > 0)
                    || (series[i].matrix && series[i].matrix.length > 0)
                    || (series[i].eventList && series[i].eventList.length > 0)
                ) {
                    return false;   // Có bất kỳ dữ liệu nào thì là dữ liệu không rỗng
                }
            }
            var loadOption = (this._option && this._option.noDataLoadingOption)
                || this._themeConfig.noDataLoadingOption
                || ecConfig.noDataLoadingOption
                || {
                    text: (this._option && this._option.noDataText)
                          || this._themeConfig.noDataText
                          || ecConfig.noDataText,
                    effect: (this._option && this._option.noDataEffect)
                            || this._themeConfig.noDataEffect
                            || ecConfig.noDataEffect
                };
            // Dữ liệu rỗng
            this.clear();
            this.showLoading(loadOption);
            return true;
        },

        /**
         * Kết xuất biểu đồ
         */
        _render: function (magicOption) {
            this._mergeGlobalConifg(magicOption);

            if (this._noDataCheck(magicOption)) {
                return;
            }

            var bgColor = magicOption.backgroundColor;
            if (bgColor) {
                if (!_canvasSupported
                    && bgColor.indexOf('rgba') != -1
                ) {
                    // Cách IE6~8 xử lý RGBA, filter sẽ gây ảnh hưởng màu sắc khác
                    var cList = bgColor.split(',');
                    this.dom.style.filter = 'alpha(opacity=' +
                        cList[3].substring(0, cList[3].lastIndexOf(')')) * 100
                        + ')';
                    cList.length = 3;
                    cList[0] = cList[0].replace('a', '');
                    this.dom.style.backgroundColor = cList.join(',') + ')';
                }
                else {
                    this.dom.style.backgroundColor = bgColor;
                }
            }

            this._zr.clearAnimation();
            this._chartList = [];

            var chartLibrary = require('./chart');
            var componentLibrary = require('./component');

            if (magicOption.xAxis || magicOption.yAxis) {
                magicOption.grid = magicOption.grid || {};
                magicOption.dataZoom = magicOption.dataZoom || {};
            }

            var componentList = [
                'title', 'legend', 'tooltip', 'dataRange', 'roamController',
                'grid', 'dataZoom', 'xAxis', 'yAxis', 'polar'
            ];

            var ComponentClass;
            var componentType;
            var component;
            for (var i = 0, l = componentList.length; i < l; i++) {
                componentType = componentList[i];
                component = this.component[componentType];

                if (magicOption[componentType]) {
                    if (component) {
                        component.refresh && component.refresh(magicOption);
                    }
                    else {
                        ComponentClass = componentLibrary.get(
                            /^[xy]Axis$/.test(componentType) ? 'axis' : componentType
                        );
                        component = new ComponentClass(
                            this._themeConfig, this._messageCenter, this._zr,
                            magicOption, this, componentType
                        );
                        this.component[componentType] = component;
                    }
                    this._chartList.push(component);
                }
                else if (component) {
                    component.dispose();
                    this.component[componentType] = null;
                    delete this.component[componentType];
                }
            }

            var ChartClass;
            var chartType;
            var chart;
            var chartMap = {};      // Ghi lại biểu đồ đã khởi tạo
            for (var i = 0, l = magicOption.series.length; i < l; i++) {
                chartType = magicOption.series[i].type;
                if (!chartType) {
                    console.error('series[' + i + '] chart type has not been defined.');
                    continue;
                }

                if (!chartMap[chartType]) {
                    chartMap[chartType] = true;
                    ChartClass = chartLibrary.get(chartType);
                    if (ChartClass) {
                        if (this.chart[chartType]) {
                            chart = this.chart[chartType];
                            chart.refresh(magicOption);
                        }
                        else {
                            chart = new ChartClass(
                                this._themeConfig, this._messageCenter, this._zr,
                                magicOption, this
                            );
                        }
                        this._chartList.push(chart);
                        this.chart[chartType] = chart;
                    }
                    else {
                        console.error(chartType + ' has not been required.');
                    }
                }
            }

            // Đã có thực thể nhưng option mới không có loại biểu đồ này thì giải phóng thực thể
            for (chartType in this.chart) {
                if (chartType != ecConfig.CHART_TYPE_ISLAND  && !chartMap[chartType]) {
                    this.chart[chartType].dispose();
                    this.chart[chartType] = null;
                    delete this.chart[chartType];
                }
            }

            this.component.grid && this.component.grid.refixAxisShape(this.component);

            this._island.refresh(magicOption);
            this._toolbox.refresh(magicOption);

            magicOption.animation && !magicOption.renderAsImage
                ? this._zr.refresh()
                : this._zr.render();

            var imgId = 'IMG' + this.id;
            var img = document.getElementById(imgId);
            if (magicOption.renderAsImage && _canvasSupported) {
                // IE8- Không hỗ trợ dạng kết xuất hình ảnh
                if (img) {
                    // Đã kết xuất rồi thì cập nhật hiển thị
                    img.src = this.getDataURL(magicOption.renderAsImage);
                }
                else {
                    // Chưa kết xuất thì chènimg dom
                    img = this.getImage(magicOption.renderAsImage);
                    img.id = imgId;
                    img.style.position = 'absolute';
                    img.style.left = 0;
                    img.style.top = 0;
                    this.dom.firstChild.appendChild(img);
                }
                this.un();
                this._zr.un();
                this._disposeChartList();
                this._zr.clear();
            }
            else if (img) {
                // Xóaimg
                img.parentNode.removeChild(img);
            }
            img = null;

            this._option = magicOption;
        },

        /**
         * Khôi phục
         */
        restore: function () {
            this._clearEffect();
            this._option = zrUtil.clone(this._optionRestore);
            this._disposeChartList();
            this._island.clear();
            this._toolbox.reset(this._option, true);
            this._render(this._option);
        },

        /**
         * Làm mới
         * @param {Object=} param, Tham số tùy chọn, dùng để kèm theo option, dùng để đồng bộ nội bộ, bên ngoài không nên đưa dữ liệu sửa đổi vào, không thể đồng bộ
         */
        refresh: function (param) {
            this._clearEffect();
            param = param || {};
            var magicOption = param.option;

            // refresh được gọi từ bên ngoài và có option đưa vào
            if (!this._refreshInside && magicOption) {
                // Thực hiện hợp nhất khác biệt đơn giản để đồng bộ bản sao dữ liệu nội bộ đang giữ, không nên đưa dữ liệu vào
                // Trong trường hợp bật thu phóng vùng dữ liệu, tính toán lại khi kéo-thả, chế độ chỉnh sửa chế độ xem dữ liệu, khi người dùng tạo ra thay đổi dữ liệu thì không thể đồng bộ
                // Nếu có đưa vào option có thay đổi dữ liệu, vui lòng setOption
                magicOption = this.getOption();
                zrUtil.merge(magicOption, param.option, true);
                zrUtil.merge(this._optionRestore, param.option, true);
                this._toolbox.reset(magicOption);
            }

            this._island.refresh(magicOption);
            this._toolbox.refresh(magicOption);

            // Dừng hoạt ảnh
            this._zr.clearAnimation();
            // Đến trước phục vụ trước, làm mới lần lượt các biểu đồ theo thứ tự, bên trong biểu đồ, refresh kiểm tra tối ưu magicOption, không cần cập nhật thì không cập nhật~
            for (var i = 0, l = this._chartList.length; i < l; i++) {
                this._chartList[i].refresh && this._chartList[i].refresh(magicOption);
            }
            this.component.grid && this.component.grid.refixAxisShape(this.component);
            this._zr.refresh();
        },

        /**
         * Giải phóng thực thể biểu đồ
         */
        _disposeChartList: function () {
            this._clearEffect();

            // Dừng hoạt ảnh
            this._zr.clearAnimation();

            var len = this._chartList.length;
            while (len--) {
                var chart = this._chartList[len];

                if (chart) {
                    var chartType = chart.type;
                    this.chart[chartType] && delete this.chart[chartType];
                    this.component[chartType] && delete this.component[chartType];
                    chart.dispose && chart.dispose();
                }
            }

            this._chartList = [];
        },

        /**
         * Thuộc tính toàn cục không phải biểu đồ merge ~~
         */
        _mergeGlobalConifg: function (magicOption) {
            var mergeList = [
                // Màu nền
                'backgroundColor',

                // Liên quan đến tính toán lại khi kéo-thả
                'calculable', 'calculableColor', 'calculableHolderColor',

                // Ký tự nối hiển thị của đảo
                'nameConnector', 'valueConnector',

                // Liên quan đến hoạt ảnh
                'animation', 'animationThreshold',
                'animationDuration', 'animationDurationUpdate',
                'animationEasing', 'addDataAnimation',

                // Danh sách kiểu hình đồ họa đánh dấu mặc định
                'symbolList',

                // Giảm độ nhạy kéo-thả của phần tử trong biểu đồ, đơn vịms, bên ngoài không nên can thiệp
                'DRAG_ENABLE_TIME'
            ];

            var len = mergeList.length;
            while (len--) {
                var mergeItem = mergeList[len];
                if (magicOption[mergeItem] == null) {
                    magicOption[mergeItem] = this._themeConfig[mergeItem] != null
                        ? this._themeConfig[mergeItem]
                        : ecConfig[mergeItem];
                }
            }

            // Danh sách màu của chuỗi số, không truyền thì dùng màu tích hợp sẵn, có thể cấu hình mảng, mượn thực thể zrender đưa vào, có nguy cơ xung đột, tạm thời như vậy
            var themeColor = magicOption.color;
            if (!(themeColor && themeColor.length)) {
                themeColor = this._themeConfig.color || ecConfig.color;
            }

            this._zr.getColor = function (idx) {
                var zrColor = require('zrender/tool/color');
                return zrColor.getColor(idx, themeColor);
            };

            if (!_canvasSupported) {
                // Không hỗ trợCanvastắt hoạt ảnh bắt buộc
                magicOption.animation = false;
                magicOption.addDataAnimation = false;
            }
        },

        /**
         * Giao diện vạn năng, cấu hình mọi tùy chọn có thể cấu hình của thực thể biểu đồ, khi gọi nhiều lần option tùy chọn, thực hiện merge và xử lý
         * @param {Object} option
         * @param {boolean=} notMerge khi gọi nhiều lần option tùy chọn mặc định được hợp nhất (merge),
         *                   nếu không cần, có thể thông qua tham số notMerge là true ngăn hợp nhất với lần trước option hợp nhất
         */
        setOption: function (option, notMerge) {
            if (!option.timeline) {
                return this._setOption(option, notMerge);
            }
            else {
                return this._setTimelineOption(option);
            }
        },

        /**
         * Giao diện vạn năng, cấu hình mọi tùy chọn có thể cấu hình của thực thể biểu đồ, khi gọi nhiều lần option tùy chọn, thực hiện merge và xử lý
         * @param {Object} option
         * @param {boolean=} notMerge khi gọi nhiều lần option tùy chọn mặc định được hợp nhất (merge),
         *                   nếu không cần, có thể thông qua tham số notMerge là true ngăn hợp nhất với lần trước option hợp nhất
         * @param {boolean=} Mặc định false. keepTimeLine biểu thị từ timeline thành phần gọi đến,
         *                   biểu thị hành vi hiện tại là timeline chuyển đổi dữ liệu, giữ timeline, 
         *                   ngược lại hủytimeline. Xem chi tiếtIssue #1601
         */
        _setOption: function (option, notMerge, keepTimeLine) {
            if (!notMerge && this._option) {
                this._option = zrUtil.merge(
                    this.getOption(),
                    zrUtil.clone(option),
                    true
                );
            }
            else {
                this._option = zrUtil.clone(option);
                !keepTimeLine && this._timeline && this._timeline.dispose();
            }

            this._optionRestore = zrUtil.clone(this._option);

            if (!this._option.series || this._option.series.length === 0) {
                this._zr.clear();
                return;
            }

            if (this.component.dataZoom                         // Tồn tại điều khiển dataZoom
                && (this._option.dataZoom                       // và option mới cũng tồn tại
                    || (this._option.toolbox
                        && this._option.toolbox.feature
                        && this._option.toolbox.feature.dataZoom
                        && this._option.toolbox.feature.dataZoom.show
                    )
                )
            ) {
                // Đồng bộ dữ liệu dataZoom
                this.component.dataZoom.syncOption(this._option);
            }
            this._toolbox.reset(this._option);
            this._render(this._option);
            return this;
        },

        /**
         * Trả về bản sao option đang hiển thị hiện tại đang giữ
         */
        getOption: function () {
            var magicOption = zrUtil.clone(this._option);

            var self = this;
            function restoreOption(prop) {
                var restoreSource = self._optionRestore[prop];

                if (restoreSource) {
                    if (restoreSource instanceof Array) {
                        var len = restoreSource.length;
                        while (len--) {
                            magicOption[prop][len].data = zrUtil.clone(
                                restoreSource[len].data
                            );
                        }
                    }
                    else {
                        magicOption[prop].data = zrUtil.clone(restoreSource.data);
                    }
                }
            }

            // Khôi phục dữ liệu trục ngang
            restoreOption('xAxis');

            // Khôi phục dữ liệu trục dọc
            restoreOption('yAxis');

            // Khôi phục dữ liệu chuỗi
            restoreOption('series');

            return magicOption;
        },

        /**
         * Giao diện nhanh để cài đặt dữ liệu
         * @param {Array} series
         * @param {boolean=} notMerge khi gọi nhiều lần option tùy chọn mặc định được hợp nhất (merge),
         *                   nếu không cần, có thể thông qua tham số notMerge là true ngăn hợp nhất với lần trước option hợp nhất.
         */
        setSeries: function (series, notMerge) {
            if (!notMerge) {
                this.setOption({series: series});
            }
            else {
                this._option.series = series;
                this.setOption(this._option, notMerge);
            }
            return this;
        },

        /**
         * Trả về bản sao series đang hiển thị hiện tại đang giữ
         */
        getSeries: function () {
            return this.getOption().series;
        },

        /**
         * timelineOptionGiao diện, cấu hình mọi tùy chọn có thể cấu hình của thực thể biểu đồ
         * @param {Object} option
         */
        _setTimelineOption: function(option) {
            this._timeline && this._timeline.dispose();
            var Timeline = require('./component/timeline');
            var timeline = new Timeline(
                this._themeConfig, this._messageCenter, this._zr, option, this
            );
            this._timeline = timeline;
            this.component.timeline = this._timeline;

            return this;
        },

        /**
         * Thêm dữ liệu động
         * Tham số hình thức là tham số dữ liệu đơn nhóm, khi nhiều nhóm thì là dữ liệu, nội dung giống[seriesIdx, data, isShift, additionData]
         * @param {number} seriesIdx Chỉ mục chuỗi
         * @param {number | Object} data Thêm dữ liệu
         * @param {boolean=} isHead Có thêm vào đầu hàng không, mặc định, không chỉ định hoặc false thì chèn vào cuối hàng
         * @param {boolean=} dataGrow Có tăng độ dài hàng đợi dữ liệu không, mặc định, không chỉ định hoặcfalsethì loại bỏ dữ liệu đối vị của mảng đích
         * @param {string=} additionData Có thêm trục hạng mục không (biểu đồ tròn là chú giải) dữ liệu, thao tác đính kèm giống isHead và dataGrow
         */
        addData: function (seriesIdx, data, isHead, dataGrow, additionData) {
            var params = seriesIdx instanceof Array
                ? seriesIdx
                : [[seriesIdx, data, isHead, dataGrow, additionData]];

            //this._optionRestore và magicOption đều phải đồng bộ
            var magicOption = this.getOption();
            var optionRestore = this._optionRestore;
            var self = this;
            for (var i = 0, l = params.length; i < l; i++) {
                seriesIdx = params[i][0];
                data = params[i][1];
                isHead = params[i][2];
                dataGrow = params[i][3];
                additionData = params[i][4];

                var seriesItem = optionRestore.series[seriesIdx];
                var inMethod = isHead ? 'unshift' : 'push';
                var outMethod = isHead ? 'pop' : 'shift';
                if (seriesItem) {
                    var seriesItemData = seriesItem.data;
                    var mSeriesItemData = magicOption.series[seriesIdx].data;

                    seriesItemData[inMethod](data);
                    mSeriesItemData[inMethod](data);
                    if (!dataGrow) {
                        seriesItemData[outMethod]();
                        data = mSeriesItemData[outMethod]();
                    }

                    if (additionData != null) {
                        var legend;
                        var legendData;

                        if (seriesItem.type === ecConfig.CHART_TYPE_PIE
                            && (legend = optionRestore.legend)
                            && (legendData = legend.data)
                        ) {
                            var mLegendData = magicOption.legend.data;
                            legendData[inMethod](additionData);
                            mLegendData[inMethod](additionData);

                            if (!dataGrow) {
                                var legendDataIdx = zrUtil.indexOf(legendData, data.name);
                                legendDataIdx != -1 && legendData.splice(legendDataIdx, 1);

                                legendDataIdx = zrUtil.indexOf(mLegendData, data.name);
                                legendDataIdx != -1 && mLegendData.splice(legendDataIdx, 1);
                            }
                        }
                        else if (optionRestore.xAxis != null && optionRestore.yAxis != null) {
                            // hạng mục trục x
                            var axisData;
                            var mAxisData;
                            var axisIdx = seriesItem.xAxisIndex || 0;

                            if (optionRestore.xAxis[axisIdx].type == null
                                || optionRestore.xAxis[axisIdx].type === 'category'
                            ) {
                                axisData = optionRestore.xAxis[axisIdx].data;
                                mAxisData = magicOption.xAxis[axisIdx].data;

                                axisData[inMethod](additionData);
                                mAxisData[inMethod](additionData);
                                if (!dataGrow) {
                                    axisData[outMethod]();
                                    mAxisData[outMethod]();
                                }
                            }

                            // hạng mục trục y
                            axisIdx = seriesItem.yAxisIndex || 0;
                            if (optionRestore.yAxis[axisIdx].type === 'category') {
                                axisData = optionRestore.yAxis[axisIdx].data;
                                mAxisData = magicOption.yAxis[axisIdx].data;

                                axisData[inMethod](additionData);
                                mAxisData[inMethod](additionData);
                                if (!dataGrow) {
                                    axisData[outMethod]();
                                    mAxisData[outMethod]();
                                }
                            }
                        }
                    }

                    // Đồng bộ trạng thái trong biểu đồ, hoạt ảnh cần
                    this._option.series[seriesIdx].data = magicOption.series[seriesIdx].data;
                }
            }

            this._zr.clearAnimation();
            var chartList = this._chartList;
            var chartAnimationCount = 0;
            var chartAnimationDone = function () {
                chartAnimationCount--;
                if (chartAnimationCount === 0) {
                    animationDone();
                }
            };
            for (var i = 0, l = chartList.length; i < l; i++) {
                if (magicOption.addDataAnimation && chartList[i].addDataAnimation) {
                    chartAnimationCount++;
                    chartList[i].addDataAnimation(params, chartAnimationDone);
                }
            }

            // Đồng bộ dữ liệu dataZoom
            this.component.dataZoom && this.component.dataZoom.syncOption(magicOption);

            this._option = magicOption;
            function animationDone() {
                if (!self._zr) {
                    return; // Đã được giải phóng
                }
                self._zr.clearAnimation();
                for (var i = 0, l = chartList.length; i < l; i++) {
                    // Có hoạt ảnh addData thì bỏ hoạt ảnh chuyển tiếp
                    chartList[i].motionlessOnce =
                        magicOption.addDataAnimation && chartList[i].addDataAnimation;
                }
                self._messageCenter.dispatch(
                    ecConfig.EVENT.REFRESH,
                    null,
                    {option: magicOption},
                    self
                );
            }

            if (!magicOption.addDataAnimation) {
                setTimeout(animationDone, 0);
            }
            return this;
        },

        /**
         * Động[đánh dấu | đường đánh dấu]Thêm
         * @param {number} seriesIdx Chỉ mục chuỗi
         * @param {Object} markData [đánh dấu | đường đánh dấu]đối tượng, hỗ trợ nhiều
         */
        addMarkPoint: function (seriesIdx, markData) {
            return this._addMark(seriesIdx, markData, 'markPoint');
        },

        addMarkLine: function (seriesIdx, markData) {
            return this._addMark(seriesIdx, markData, 'markLine');
        },

        _addMark: function (seriesIdx, markData, markType) {
            var series = this._option.series;
            var seriesItem;

            if (series && (seriesItem = series[seriesIdx])) {
                var seriesR = this._optionRestore.series;
                var seriesRItem = seriesR[seriesIdx];
                var markOpt = seriesItem[markType];
                var markOptR = seriesRItem[markType];

                markOpt = seriesItem[markType] = markOpt || {data: []};
                markOptR = seriesRItem[markType] = markOptR || {data: []};

                for (var key in markData) {
                    if (key === 'data') {
                        // dữ liệuconcat
                        markOpt.data = markOpt.data.concat(markData.data);
                        markOptR.data = markOptR.data.concat(markData.data);
                    }
                    else if (typeof markData[key] != 'object' || markOpt[key] == null) {
                        // Kiểu đơn giản hoặc giá trị mới gán trực tiếp
                        markOpt[key] = markOptR[key] = markData[key];
                    }
                    else {
                        // Đối tượng phức tạp không phải dữ liệumerge
                        zrUtil.merge(markOpt[key], markData[key], true);
                        zrUtil.merge(markOptR[key], markData[key], true);
                    }
                }

                var chart = this.chart[seriesItem.type];
                chart && chart.addMark(seriesIdx, markData, markType);
            }

            return this;
        },

        /**
         * Động[đánh dấu | đường đánh dấu]Xóa
         * @param {number} seriesIdx Chỉ mục chuỗi
         * @param {string} markName [đánh dấu | đường đánh dấu]Tên
         */
        delMarkPoint: function (seriesIdx, markName) {
            return this._delMark(seriesIdx, markName, 'markPoint');
        },

        delMarkLine: function (seriesIdx, markName) {
            return this._delMark(seriesIdx, markName, 'markLine');
        },

        _delMark: function (seriesIdx, markName, markType) {
            var series = this._option.series;
            var seriesItem;
            var mark;
            var dataArray;

            if (!(
                    series
                    && (seriesItem = series[seriesIdx])
                    && (mark = seriesItem[markType])
                    && (dataArray = mark.data)
                )
            ) {
                return this;
            }

            markName = markName.split(' > ');
            var targetIndex = -1;

            for (var i = 0, l = dataArray.length; i < l; i++) {
                var dataItem = dataArray[i];
                if (dataItem instanceof Array) {
                    if (dataItem[0].name === markName[0]
                        && dataItem[1].name === markName[1]
                    ) {
                        targetIndex = i;
                        break;
                    }
                }
                else if (dataItem.name === markName[0]) {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex > -1) {
                dataArray.splice(targetIndex, 1);
                this._optionRestore.series[seriesIdx][markType].data.splice(targetIndex, 1);

                var chart = this.chart[seriesItem.type];
                chart && chart.delMark(seriesIdx, markName.join(' > '), markType);
            }

            return this;
        },

        /**
         * Lấydom
         */
        getDom: function () {
            return this.dom;
        },

        /**
         * Lấyzrenderthực thể, có thể dùng để thêmshapevà điều khiển độ sâu
         */
        getZrender: function () {
            return this._zr;
        },

        /**
         * Lấy Base64 hình ảnh dataURL
         * @param {string} imgType Kiểu hình ảnh, hỗ trợ png|jpeg, mặc định là png
         * @return imgDataURL
         */
        getDataURL: function (imgType) {
            if (!_canvasSupported) {
                return '';
            }

            if (this._chartList.length === 0) {
                // Kết xuất thành hình ảnh
                var imgId = 'IMG' + this.id;
                var img = document.getElementById(imgId);
                if (img) {
                    return img.src;
                }
            }

            // Xóatooltipphần tử
            var tooltip = this.component.tooltip;
            tooltip && tooltip.hideTip();

            switch (imgType) {
                case 'jpeg':
                    break;
                default:
                    imgType = 'png';
            }

            var bgColor = this._option.backgroundColor;
            if (bgColor && bgColor.replace(' ','') === 'rgba(0,0,0,0)') {
                bgColor = '#fff';
            }

            return this._zr.toDataURL('image/' + imgType, bgColor);
        },

        /**
         * Lấyimg
         * @param {string} imgType Kiểu hình ảnh, hỗ trợ png|jpeg, mặc định là png
         * @return img dom
         */
        getImage: function (imgType) {
            var title = this._optionRestore.title;
            var imgDom = document.createElement('img');
            imgDom.src = this.getDataURL(imgType);
            imgDom.title = (title && title.text) || 'ECharts';
            return imgDom;
        },

        /**
         * Lấy Base64 hình ảnh dataURL
         * @param {string} imgType Kiểu hình ảnh, hỗ trợ png|jpeg, mặc định là png
         * @return imgDataURL
         */
        getConnectedDataURL: function (imgType) {
            if (!this.isConnected()) {
                return this.getDataURL(imgType);
            }

            var tempDom = this.dom;
            var imgList = {
                'self': {
                    img: this.getDataURL(imgType),
                    left: tempDom.offsetLeft,
                    top: tempDom.offsetTop,
                    right: tempDom.offsetLeft + tempDom.offsetWidth,
                    bottom: tempDom.offsetTop + tempDom.offsetHeight
                }
            };

            var minLeft = imgList.self.left;
            var minTop = imgList.self.top;
            var maxRight = imgList.self.right;
            var maxBottom = imgList.self.bottom;

            for (var c in this._connected) {
                tempDom = this._connected[c].getDom();
                imgList[c] = {
                    img: this._connected[c].getDataURL(imgType),
                    left: tempDom.offsetLeft,
                    top: tempDom.offsetTop,
                    right: tempDom.offsetLeft + tempDom.offsetWidth,
                    bottom: tempDom.offsetTop + tempDom.offsetHeight
                };

                minLeft = Math.min(minLeft, imgList[c].left);
                minTop = Math.min(minTop, imgList[c].top);
                maxRight = Math.max(maxRight, imgList[c].right);
                maxBottom = Math.max(maxBottom, imgList[c].bottom);
            }

            var zrDom = document.createElement('div');
            zrDom.style.position = 'absolute';
            zrDom.style.left = '-4000px';
            zrDom.style.width = (maxRight - minLeft) + 'px';
            zrDom.style.height = (maxBottom - minTop) + 'px';
            document.body.appendChild(zrDom);

            var zrImg = require('zrender').init(zrDom);

            var ImageShape = require('zrender/shape/Image');
            for (var c in imgList) {
                zrImg.addShape(new ImageShape({
                    style: {
                        x: imgList[c].left - minLeft,
                        y: imgList[c].top - minTop,
                        image: imgList[c].img
                    }
                }));
            }

            zrImg.render();
            var bgColor = this._option.backgroundColor;
            if (bgColor && bgColor.replace(/ /g, '') === 'rgba(0,0,0,0)') {
                bgColor = '#fff';
            }

            var image = zrImg.toDataURL('image/png', bgColor);

            setTimeout(function () {
                zrImg.dispose();
                zrDom.parentNode.removeChild(zrDom);
                zrDom = null;
            }, 100);

            return image;
        },

        /**
         * Lấyimg
         * @param {string} imgType Kiểu hình ảnh, hỗ trợ png|jpeg, mặc định là png
         * @return img dom
         */
        getConnectedImage: function (imgType) {
            var title = this._optionRestore.title;
            var imgDom = document.createElement('img');
            imgDom.src = this.getConnectedDataURL(imgType);
            imgDom.title = (title && title.text) || 'ECharts';
            return imgDom;
        },

        /**
         * Giao diện bên ngoài gắn sự kiện
         * @param {Object} eventName Tên sự kiện
         * @param {Object} eventListener Hàm phản hồi sự kiện
         */
        on: function (eventName, eventListener) {
            this._messageCenterOutSide.bind(eventName, eventListener, this);
            return this;
        },

        /**
         * Giao diện bên ngoài gỡ gắn sự kiện
         * @param {Object} eventName Tên sự kiện
         * @param {Object} eventListener Hàm phản hồi sự kiện
         */
        un: function (eventName, eventListener) {
            this._messageCenterOutSide.unbind(eventName, eventListener);
            return this;
        },

        /**
         * Liên động nhiều biểu đồ
         * @param connectTarget{ECharts | Array <ECharts>} connectTarget Mục tiêu liên động
         */
        connect: function (connectTarget) {
            if (!connectTarget) {
                return this;
            }

            if (!this._connected) {
                this._connected = {};
            }

            if (connectTarget instanceof Array) {
                for (var i = 0, l = connectTarget.length; i < l; i++) {
                    this._connected[connectTarget[i].id] = connectTarget[i];
                }
            }
            else {
                this._connected[connectTarget.id] = connectTarget;
            }

            return this;
        },

        /**
         * Gỡ liên động nhiều biểu đồ
         * @param connectTarget{ECharts | Array <ECharts>} connectTarget Gỡ mục tiêu liên động
         */
        disConnect: function (connectTarget) {
            if (!connectTarget || !this._connected) {
                return this;
            }

            if (connectTarget instanceof Array) {
                for (var i = 0, l = connectTarget.length; i < l; i++) {
                    delete this._connected[connectTarget[i].id];
                }
            }
            else {
                delete this._connected[connectTarget.id];
            }

            for (var k in this._connected) {
                return k, this; // Không rỗng
            }

            // Rỗng thì chuyển thành cờ đánh dấu
            this._connected = false;
            return this;
        },

        /**
         * Phản hồi sự kiện liên động
         */
        connectedEventHandler: function (param) {
            if (param.__echartsId != this.id) {
                // Sự kiện từ biểu đồ liên động khác
                this._onevent(param);
            }
        },

        /**
         * Có tồn tại liên động nhiều biểu đồ không
         */
        isConnected: function () {
            return !!this._connected;
        },

        /**
         * Hiển thị loading chuyển tiếp
         * @param {Object} loadingOption
         */
        showLoading: function (loadingOption) {
            var effectList = {
                bar: require('zrender/loadingEffect/Bar'),
                bubble: require('zrender/loadingEffect/Bubble'),
                dynamicLine: require('zrender/loadingEffect/DynamicLine'),
                ring: require('zrender/loadingEffect/Ring'),
                spin: require('zrender/loadingEffect/Spin'),
                whirling: require('zrender/loadingEffect/Whirling')
            };
            this._toolbox.hideDataView();

            loadingOption = loadingOption || {};

            var textStyle = loadingOption.textStyle || {};
            loadingOption.textStyle = textStyle;

            var finalTextStyle = zrUtil.merge(
                zrUtil.merge(
                    zrUtil.clone(textStyle),
                    this._themeConfig.textStyle
                ),
                ecConfig.textStyle
            );

            textStyle.textFont = finalTextStyle.fontStyle + ' '
                                 + finalTextStyle.fontWeight + ' '
                                 + finalTextStyle.fontSize + 'px '
                                 + finalTextStyle.fontFamily;

            textStyle.text = loadingOption.text
                             || (this._option && this._option.loadingText)
                             || this._themeConfig.loadingText
                             || ecConfig.loadingText;

            if (loadingOption.x != null) {
                textStyle.x = loadingOption.x;
            }
            if (loadingOption.y != null) {
                textStyle.y = loadingOption.y;
            }

            loadingOption.effectOption = loadingOption.effectOption || {};
            loadingOption.effectOption.textStyle = textStyle;

            var Effect = loadingOption.effect;
            if (typeof Effect === 'string' || Effect == null) {
                Effect =  effectList[
                              loadingOption.effect
                              || (this._option && this._option.loadingEffect)
                              || this._themeConfig.loadingEffect
                              || ecConfig.loadingEffect
                          ]
                          || effectList.spin;
            }
            this._zr.showLoading(new Effect(loadingOption.effectOption));
            return this;
        },

        /**
         * Ẩnloadingchuyển tiếp
         */
        hideLoading: function () {
            this._zr.hideLoading();
            return this;
        },

        /**
         * Cài đặt chủ đề
         */
        setTheme: function (theme) {
            if (theme) {
               if (typeof theme === 'string') {
                    // Chủ đề mặc định
                    switch (theme) {
                        case 'macarons':
                            theme = require('./theme/macarons');
                            break;
                        case 'infographic':
                            theme = require('./theme/infographic');
                            break;
                        default:
                            theme = {}; // require('./theme/default');
                    }
                }
                else {
                    theme = theme || {};
                }

                // // Đặt lại cấu hình mặc định
                // // this._themeConfigsẽ bị đối tượng khác tham chiếu và giữ
                // // vì vậy không thể đổi thànhthis._themeConfig = {};
                // for (var key in this._themeConfig) {
                //     delete this._themeConfig[key];
                // }
                // for (var key in ecConfig) {
                //     this._themeConfig[key] = zrUtil.clone(ecConfig[key]);
                // }

                // // Mảng màu theo theme, không merge
                // theme.color && (this._themeConfig.color = []);

                // // Danh sách kiểu hình đồ họa đánh dấu mặc định, không merge
                // theme.symbolList && (this._themeConfig.symbolList = []);

                // // Áp dụng chủ đề mới
                // zrUtil.merge(this._themeConfig, zrUtil.clone(theme), true);
                this._themeConfig = theme;
            }

            if (!_canvasSupported) {   // IE8-
                var textStyle = this._themeConfig.textStyle;
                textStyle && textStyle.fontFamily && textStyle.fontFamily2
                    && (textStyle.fontFamily = textStyle.fontFamily2);

                textStyle = ecConfig.textStyle;
                textStyle.fontFamily = textStyle.fontFamily2;
            }

            this._timeline && this._timeline.setTheme(true);
            this._optionRestore && this.restore();
        },

        /**
         * Cập nhật khi kích thước vùng xem thay đổi, không gắn mặc định, bên sử dụng gọi khi cần
         */
        resize: function () {
            var self = this;
            return function(){
                self._clearEffect();
                self._zr.resize();
                if (self._option && self._option.renderAsImage && _canvasSupported) {
                    // Kết xuất thành hình ảnh rồi đi lại theo render chế độ
                    self._render(self._option);
                    return self;
                }
                // Dừng hoạt ảnh
                self._zr.clearAnimation();
                self._island.resize();
                self._toolbox.resize();
                self._timeline && self._timeline.resize();
                // Đến trước phục vụ trước, không thể chỉ làm mới chính mình, cũng không thể làm mới trong vòng lặp trước, vì dữ liệu hệ tọa độ thay đổi sẽ ảnh hưởng đến kích thước của biểu đồ khác
                // Vì vậy làm mới lần lượt các biểu đồ theo thứ tự, bên trong biểu đồ, refresh kiểm tra tối ưu, không cần cập nhật thì không cập nhật~
                for (var i = 0, l = self._chartList.length; i < l; i++) {
                    self._chartList[i].resize && self._chartList[i].resize();
                }
                self.component.grid && self.component.grid.refixAxisShape(self.component);
                self._zr.refresh();
                self._messageCenter.dispatch(ecConfig.EVENT.RESIZE, null, null, self);
                return self;
            };
        },

        _clearEffect: function() {
            this._zr.modLayer(ecConfig.EFFECT_ZLEVEL, { motionBlur: false });
            this._zr.painter.clearLayer(ecConfig.EFFECT_ZLEVEL);
        },

        /**
         * Xóa nội dung đã kết xuất , clearsauechartsthực thể có thể dùng được
         */
        clear: function () {
            this._disposeChartList();
            this._zr.clear();
            this._option = {};
            this._optionRestore = {};
            this.dom.style.backgroundColor = null;
            return this;
        },

        /**
         * Giải phóng,disposesauechartsthực thể không thể dùng được
         */
        dispose: function () {
            var key = this.dom.getAttribute(DOM_ATTRIBUTE_KEY);
            key && delete _instances[key];

            this._island.dispose();
            this._toolbox.dispose();
            this._timeline && this._timeline.dispose();
            this._messageCenter.unbind();
            this.clear();
            this._zr.dispose();
            this._zr = null;
        }
    };

    return self;
});