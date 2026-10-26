/* newtouch 浮层选择器 */
$.fn.multiquencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: '',
        ajaxreqdata: null,
        //function post 或者 jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //是否使用jsonp
        isinputchangetriggered: true,
        //Cóinput change触发的（Nếu so với Nút nhấn chuột 浮层出来，Thì là truyềnfalse）
        inputtextcheck: null,
        //Doinput changeKích hoạt，văn bản của nhập xác minh trước có thể function
        mutiselect: false,
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('');
        if (options.id) {
            options.id = "" + options.id;
        }
    }
    if (!options.id) {
        //随机生成id
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "" + idtemp;
    }
    if (!options.id) {
        throw new Error("");
        return;
    }

    options.$this.attr('', ''); //关联浮层
    //Tạo lớp nổi
    options.create = function () {
        var $divContainer = $('').find('' + options.id + '');
        if ($divContainer.length == 0) {
            $('').append('' + options.id + '');
            var $divContainer = $('').find('' + options.id);
            $divContainer.css({
                height: (options.height + 8),
                width: options.width
            });
            //add by sunny 20180807 Căn chỉnh dòng mỗi tần suất
            var className = "";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('' + className + '');
            //如果多选，增加确定关闭按钮
            if (options.mutiselect) {
                $divContainer.append('');
            }
            $divContainer.append('');
            if (options.tableWidth) {
                $divContainer.find('').css('', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//绑定多选的确认关闭事件
        }
        else {
            options.container = $divContainer;
        }

    };
    //给浮层定位（Theoelementvị trí）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('', options.X);
            options.container.css('', options.Y);
        } else {
            //Theooptions.$this的坐标 来定位
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('', "");
            } else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    options.container.css('', (thisDomPoint.l) + "");
                } else {
                    options.container.css('', (thisDomPoint.l - options.width + options.$this.width() + 5) + "");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //下方不够用,置到上方
                options.container.css('', (thisDomPoint.t - options.height - 3) + "");
            } else {
                options.container.css('', (thisDomPoint.t + options.$this.height() + 2) + "");
            }
        }
    };
    //Tải dữ liệu
    options.loaddata = function () {
        if (data) {
            (options.funcBindRowData)(data);
        }
        else if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) {
                    var resultObjArr = (options.filter)(options.$this.val());
                } else {
                    var resultObjArr = options.filter();
                } (options.funcBindRowData)(resultObjArr);
            }
        } else {
            //Thông qua dữ liệu lấy giao diện
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this); //Ai đã kích hoạt，Trả về cho trang
                if (thisParameters) {
                    thisUrl += "" + thisParameters;
                }
            }
            if (options.ajaxreqdata) {
                var thisReqData = options.ajaxreqdata();
            }

            options.lastRequestMark = thisUrl + (!!thisReqData ? thisReqData.toString() : '');

            if (!options.isjsonp) {
                $.ajax({
                    type: options.ajaxmethod,
                    url: thisUrl,
                    data: thisReqData,
                    dataType: "",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {
                            //Hiển thị lộn xộn ngăn
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }
                            if (options.filter) {
                                data = (options.filter)(data);
                            } (options.funcBindRowData)(data);
                        } else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            } else {
                //jsonpTải dữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: '',
                    jsonp: '',
                    jsonpCallback: "",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "" && data.data) {

                            //Hiển thị lộn xộn ngăn
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }

                            if (!(data.data instanceof Object)) {
                                data.data = JSON.parse(data.data);
                            }
                            if (options.filter) {
                                (options.funcBindRowData)((options.filter)(data.data));
                            } (options.funcBindRowData)(data.data);
                        } else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
        }
    };
    //得到返回结果，遍历呈现之
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
            }
            if (options.height) {
                options.container.find('').css('', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '' + resultObjArr[i][options.showtext] + "";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('' + this, resultObjArr[i][this]);   //data- 属性赋值
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === '') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('');
                }
                $li.appendTo(options.container.find(''));
            }
        }
    }
    var selectorsList = [];
    //bindSự kiện đã chọn nhấn chuột
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('', '',
                function () {
                    if (options.mutiselect) { //多选
                        if ($(this).hasClass('')) { //Hủy đã chọn
                            $(this).removeClass('');
                        } else { //Đã chọn 
                            $(this).addClass('');
                        };
                    } else //单选
                    {
                        options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt，Trả về cho trang
                        if (options.isinputchangetriggered) {
                            options.$this.trigger(''); //如果是input本身的changeKích hoạt 给文本框焦点
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //双击一行 处理完之后
                        }
                        options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
                        options.container.hide();
                    }
                });
        }
    }

    options.bindclick = function () {
        //Xác nhận 
        options.container.find('').click(function () {
            selectorsList = [];
            options.container.find('').each(function () {
                if ($(this).hasClass('')) {
                    selectorsList.push($(this));
                }
            });
            options.itemdbclickhandler(selectorsList, options.$this); //Ai đã kích hoạt，Trả về cho trang
            options.container.find('').trigger('');
        });

        //Đóng
        options.container.find('').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger(''); //如果是input本身的changeKích hoạt 给文本框焦点
            }
            options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
            options.container.hide();
        });
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location(); //定位
        options.container.show(); //定位好 Hiển thị lại
        options.loaddata(); //Tải dữ liệu
        options.binddbclickchooseevent(); //双击选中事件  //应该是触发一次就可以了
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu，Ẩn nó
                }
                if (typeof options.inputtextcheck === '') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }
                options.okload();
            });
        } else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                $('').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu，Ẩn nó
                }
                if (typeof options.inputtextcheck === '') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }
                options.okload();
            });
        }

        options.$this.keyup(function (event) { //Phải làkeyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }
            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //值没有变  则不触发
            }

            //Ẩn trước
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạiulNội dung
            }
            $('').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu，Ẩn nó
            }
            if (typeof options.inputtextcheck === '') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bind键盘上下事件
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find(''); //tất cả của trong lớp nổi hiện tạitr
                if ($trs.length == 0) {
                    return; //Dòng không có
                }
                var curActiveIndex = -1; //Hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0; //Mục tiêuindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                } else {
                    if (event.keyCode == 37) {
                        //alert("");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnhsrollvị trí
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('').scrollTop(scrollTop - 50);

                $trs.removeClass('');
                $($trs[dstnIndex]).addClass('');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find(''); //tất cả của trong lớp nổi hiện tạili
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('');
                    }
                }
            }
        });

        //Khác nhấn chuột Ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('') === '')) {
                return;
            }
            if ($target.parents('').length == 0 && $('').length > 0) {
                $('').hide();
            }
        })
    } else {
        //Doclick事件来触发
        options.$this.click(function () {
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
            }
            $('').hide();
            options.okload();
        });

        //Khác nhấn chuột Ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('') === '')) {
                return;
            }
            if ($target.parents('').length == 0 && $('').length > 0) {
                $('').hide();
            }
        })
    }

};

/* 频次浮层选择器 */
$.fn.frequencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: '',
        ajaxreqdata: null,  //function post 或者 jsonp 
        ajaxsync: false,
        ajaxparameters: null,    //function get
        isjsonp: false,  //是否使用jsonp
        isinputchangetriggered: true,    //Cóinput change触发的（Nếu so với Nút nhấn chuột 浮层出来，Thì là truyềnfalse）
        inputtextcheck: null,   //Doinput changeKích hoạt，văn bản của nhập xác minh trước có thể function
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('');
        if (options.id) {
            options.id = "" + options.id;
        }
    }
    if (!options.id) {
        //随机生成id
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        console.log(idtemp);
        options.id = "" + idtemp;
    }
    if (!options.id) {
        throw new Error("");
        return;
    }

    options.$this.attr('', ''); //关联浮层

    //Tạo lớp nổi
    options.create = function () {
        var $divContainer = $('').find('' + options.id + '');
        if ($divContainer.length == 0) {
            $('').append('' + options.id + '');
            var $divContainer = $('').find('' + options.id);
            $divContainer.css({
                height: (options.height + 2),
                width: options.width
            });
            //add by sunny 20180807 Căn chỉnh dòng mỗi tần suất
            var className = "";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('' + className + '');
            if (options.tableWidth) {
                $divContainer.find('').css('', options.tableWidth);
            }
        }
        options.container = $divContainer;
    };
    //给浮层定位（Theoelementvị trí）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('', options.X);
            options.container.css('', options.Y);
        }
        else {
            //Theooptions.$this的坐标 来定位
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('', "");
            }
            else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    options.container.css('', (thisDomPoint.l) + "");
                }
                else {
                    options.container.css('', (thisDomPoint.l - options.width + options.$this.width() + 5) + "");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //下方不够用,置到上方
                options.container.css('', (thisDomPoint.t - options.height - 3) + "");
            }
            else {
                options.container.css('', (thisDomPoint.t + options.$this.height() + 2) + "");
            }
        }
    };
    //Tải dữ liệu
    options.loaddata = function () {
        if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) {
                    var resultObjArr = (options.filter)(options.$this.val());
                }
                else {
                    var resultObjArr = options.filter();
                }
                (options.funcBindRowData)(resultObjArr);
            }
        }
        else {
            //Thông qua dữ liệu lấy giao diện
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //Ai đã kích hoạt，Trả về cho trang
                if (thisParameters) {
                    thisUrl += "" + thisParameters;
                }
            }
            if (options.ajaxreqdata) {
                var thisReqData = options.ajaxreqdata();
            }

            options.lastRequestMark = thisUrl + (!!thisReqData ? thisReqData.toString() : '');

            if (!options.isjsonp) {
                $.ajax({
                    type: options.ajaxmethod,
                    url: thisUrl,
                    data: thisReqData,
                    dataType: "",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {

                            //Hiển thị lộn xộn ngăn
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                //console.log("");
                                return;
                            }
                            if (options.filter) {
                                data = (options.filter)(data);
                            }
                            (options.funcBindRowData)(data);
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
            else {
                //jsonpTải dữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: '',
                    jsonp: '',
                    jsonpCallback: "",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "" && data.data) {

                            //Hiển thị lộn xộn ngăn
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }

                            if (!(data.data instanceof Object)) {
                                data.data = JSON.parse(data.data);
                            }
                            if (options.filter) {
                                (options.funcBindRowData)((options.filter)(data.data));
                            }
                            (options.funcBindRowData)(data.data);
                        }
                        else {
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            }
        }
    };
    //得到返回结果，遍历呈现之
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
            }
            if (options.height) {
                options.container.find('').css('', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '' + resultObjArr[i][options.showtext] + "";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('' + this, resultObjArr[i][this]);   //data- 属性赋值
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === '') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('');
                }
                options.showtitle = '';
                if (options.showtitle && resultObjArr[i][options.showtitle]) {
                    $li.attr('', resultObjArr[i][options.showtitle]);
                }
                $li.appendTo(options.container.find(''));
            }
        }
    }
    //bindSự kiện đã chọn nhấn chuột
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('', '', function () {
                options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt，Trả về cho trang
                if (options.isinputchangetriggered) {
                    //如果是input本身的changeKích hoạt 给文本框焦点
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //双击一行 处理完之后
                }
                options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location();   //定位
        options.container.show();   //定位好 Hiển thị lại
        options.loaddata();   //Tải dữ liệu
        options.binddbclickchooseevent();   //双击选中事件  //应该是触发一次就可以了
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu，Ẩn nó
                }
                if (typeof options.inputtextcheck === '') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }
        else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                if (options.focusfromdbclickitem === true) {
                    options.focusfromdbclickitem = false;   //不能循环触发focus
                    return;
                }

                $('').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu，Ẩn nó
                }
                if (typeof options.inputtextcheck === '') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }

        options.$this.keyup(function (event) {   //Phải làkeyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }

            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //值没有变  则不触发
            }

            //Ẩn trước
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạiulNội dung
            }
            $('').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu，Ẩn nó
            }
            if (typeof options.inputtextcheck === '') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //bind键盘上下事件
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('');    //tất cả của trong lớp nổi hiện tạitr
                if ($trs.length == 0) {
                    return; //Dòng không có
                }
                var curActiveIndex = -1;    //Hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0;  //Mục tiêuindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                }
                else {
                    if (event.keyCode == 37) {
                        //alert("");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    }
                    else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnhsrollvị trí
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('').scrollTop(scrollTop - 50);

                $trs.removeClass('');
                $($trs[dstnIndex]).addClass('');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('');    //tất cả của trong lớp nổi hiện tạili
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('');
                    }
                }
            }
        });

        //Khác nhấn chuột Ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('') === '')) {
                return;
            }
            if ($target.parents('').length == 0 && $('').length > 0) {
                $('').hide();
            }
        })
    }
    else {
        //Doclick事件来触发
        options.$this.click(function () {
            if (options.container) {
                options.container.find('').html(''); //Xóa hiện tạitbodyNội dung
            }
            $('').hide();

            options.okload();
        });

        //Khác nhấn chuột Ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('') === '')) {
                return;
            }
            if ($target.parents('').length == 0 && $('').length > 0) {
                $('').hide();
            }
        })
    }
};