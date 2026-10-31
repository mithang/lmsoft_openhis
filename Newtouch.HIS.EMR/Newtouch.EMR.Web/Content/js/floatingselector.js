/* newtouch Bộ chọn lớp nổi */
$.fn.multiquencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: 'GET',
        ajaxreqdata: null,
        //function post hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //Có sử dụng hay khôngjsonp
        isinputchangetriggered: true,
        //Cóinput changeđược kích hoạt(ví dụ như nhấn nút để lớp nổi hiện ra,tức là truyềnfalse)
        inputtextcheck: null,
        //Bởiinput changekích hoạt,có thể kiểm tra trước văn bản đã nhập function
        mutiselect: false,
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('id');
        if (options.id) {
            options.id = "divfloat_" + options.id;
        }
    }
    if (!options.id) {
        //Tạo ngẫu nhiênid
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //Lớp nổi liên quan
    //Tạo lớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 8),
                width: options.width
            });
            //add by sunny 20180807 Căn chỉnh từng dòng theo tần suất
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className+'"><ul></ul>');
            //Nếu chọn nhiều,thêm nút xác nhận đóng
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="Xác nhận" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "Đóng" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//Liên kết sự kiện xác nhận đóng khi chọn nhiều
        }
        else {
            options.container = $divContainer;
        }

    };
    //Định vị lớp nổi(Theoelementvị trí)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //Theooptions.$thisđịnh vị theo tọa độ
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            } else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    options.container.css('left', (thisDomPoint.l) + "px");
                } else {
                    options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //Phía dưới không đủ chỗ,đặt lên phía trên
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            } else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //Tải dữ liệu
    options.loaddata = function () {
        if (!options.url) {
            if (options.filter) {
                if (options.isinputchangetriggered) {
                    var resultObjArr = (options.filter)(options.$this.val());
                } else {
                    var resultObjArr = options.filter();
                } (options.funcBindRowData)(resultObjArr);
            }
        } else {
            //Lấy dữ liệu qua giao diện
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this); //Ai đã kích hoạt,trả về cho trang
                if (thisParameters) {
                    thisUrl += "?" + thisParameters;
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
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {
                            //Ngăn hiển thị lộn xộn
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
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn hiển thị lộn xộn
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
    //Nhận được kết quả trả về,duyệt và hiển thị
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //Xóa nội dung tbody
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Gán thuộc tính
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === 'function') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('active');
                }
                $li.appendTo(options.container.find('ul'));
            }
        }
    }
    var selectorsList = [];
    //bindSự kiện nhấn chọn
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li',
                function () {
                    if (options.mutiselect) { //Chọn nhiều
                        if ($(this).hasClass('active')) { //Hủy chọn
                            $(this).removeClass('active');
                        } else { //Đã chọn 
                            $(this).addClass('active');
                        };
                    } else //Chọn một
                    {
                        options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt,trả về cho trang
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //Nếu change của chính input kích hoạt thì focus vào hộp văn bản
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //Sau khi xử lý xong khi nhấn đúp một dòng
                        }
                        options.container.find('ul').html(''); //Xóa nội dung tbody
                        options.container.hide();
                    }
                });
        }
    }

    options.bindclick = function () {
        //Xác nhận 
        options.container.find('.btn_submit').click(function () {
            selectorsList = [];
            options.container.find('li').each(function () {
                if ($(this).hasClass('active')) {
                    selectorsList.push($(this));
                }
            });
            options.itemdbclickhandler(selectorsList, options.$this); //Ai đã kích hoạt,trả về cho trang
            options.container.find('.btn_close').trigger('click');
        });

        //Đóng
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //Nếu change của chính input kích hoạt thì focus vào hộp văn bản
            }
            options.container.find('ul').html(''); //Xóa nội dung tbody
            options.container.hide();
        });
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location(); //Định vị
        options.container.show(); //Định vị xong rồi hiển thị
        options.loaddata(); //Tải dữ liệu
        options.binddbclickchooseevent(); //Sự kiện nhấn đúp chọn  //lẽ ra chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu,ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }
                options.okload();
            });
        } else if (options.focusautotrigger === true) {
            options.$this.focus(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu,ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
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
                return; //Giá trị không thay đổi  thì không kích hoạt
            }

            //Ẩn trước
            if (options.container) {
                options.container.find('ul').html(''); //Xóaulnội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu,ẩn đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bindSự kiện phím lên/xuống
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //Tất cả các tr trong lớp nổi hiện tại
                if ($trs.length == 0) {
                    return; //Không có dòng
                }
                var curActiveIndex = -1; //hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0; //Đíchindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                } else {
                    if (event.keyCode == 37) {
                        //alert("Đã nhấn←phím!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("Đã nhấn →phím!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("Đã nhấn↑phím!");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("Đã nhấn↓phím!");
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
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //Tất cả các li trong lớp nổi hiện tại
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //Nhấn vào chỗ khác để ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    } else {
        //Bởiclickkích hoạt bằng sự kiện
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //Xóa nội dung tbody
            }
            $('body div.ui-nfs:visible').hide();
            options.okload();
        });

        //Nhấn vào chỗ khác để ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }

};

/* Bộ chọn lớp nổi tần suất */
$.fn.frequencyNewtouchFloatingSelector = function (options, data) {
    var defaults = {
        height: 400,
        width: 100,
        X: 0,
        Y: 0,
        id: null,
        fiter: null,
        url: null,
        ajaxmethod: 'GET',
        ajaxreqdata: null,  //function post hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,    //function get
        isjsonp: false,  //Có sử dụng hay khôngjsonp
        isinputchangetriggered: true,    //Cóinput changeđược kích hoạt(ví dụ như nhấn nút để lớp nổi hiện ra,tức là truyềnfalse)
        inputtextcheck: null,   //Bởiinput changekích hoạt,có thể kiểm tra trước văn bản đã nhập function
    };
    var options = $.extend(defaults, options);
    options.$this = $(this);
    if (options.isinputchangetriggered) {
        options.prveValue = options.$this.val();
    }
    if (!options.id) {
        options.id = options.$this.attr('id');
        if (options.id) {
            options.id = "divfloat_" + options.id;
        }
    }
    if (!options.id) {
        //Tạo ngẫu nhiênid
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        console.log(idtemp);
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //Lớp nổi liên quan

    //Tạo lớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 2),
                width: options.width
            });
            //add by sunny 20180807 Căn chỉnh từng dòng theo tần suất
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className + '"><ul></ul>');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
        }
        options.container = $divContainer;
    };
    //Định vị lớp nổi(Theoelementvị trí)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        }
        else {
            //Theooptions.$thisđịnh vị theo tọa độ
            var thisDomPoint = getDOMPositionPoint(options.$this[0]);

            if (options.width > ($(document).width() / 2 + $(document).width() / 4)) {
                options.container.css('left', "10px");
            }
            else {
                if (thisDomPoint.l < ($(document).width() / 2)) {
                    options.container.css('left', (thisDomPoint.l) + "px");
                }
                else {
                    options.container.css('left', (thisDomPoint.l - options.width + options.$this.width() + 5) + "px");
                }
            }

            if (($(document).height() - thisDomPoint.t - options.$this.height() - 5) < options.height) {
                //Phía dưới không đủ chỗ,đặt lên phía trên
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            }
            else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
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
            //Lấy dữ liệu qua giao diện
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //Ai đã kích hoạt,trả về cho trang
                if (thisParameters) {
                    thisUrl += "?" + thisParameters;
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
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        if (data && data.length && data.length > 0) {

                            //Ngăn hiển thị lộn xộn
                            if (!!options.lastRequestMark
                                && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                //console.log("skip bind data");
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
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn hiển thị lộn xộn
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
    //Nhận được kết quả trả về,duyệt và hiển thị
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //Xóa nội dung tbody
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Gán thuộc tính
                });
                var itemActived = false;
                if (typeof options.checkItemActivity === 'function') {
                    itemActived = options.checkItemActivity($li, options.$this);
                }
                if (itemActived) {
                    $li.addClass('active');
                }
                options.showtitle = 'yzpcmcsm';
                if (options.showtitle && resultObjArr[i][options.showtitle]) {
                    $li.attr('title', resultObjArr[i][options.showtitle]);
                }
                $li.appendTo(options.container.find('ul'));
            }
        }
    }
    //bindSự kiện nhấn chọn
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li', function () {
                options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt,trả về cho trang
                if (options.isinputchangetriggered) {
                    //Nếu change của chính input kích hoạt thì focus vào hộp văn bản
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //Sau khi xử lý xong khi nhấn đúp một dòng
                }
                options.container.find('ul').html(''); //Xóa nội dung tbody
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        //debugger
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location();   //Định vị
        options.container.show();   //Định vị xong rồi hiển thị
        options.loaddata();   //Tải dữ liệu
        options.binddbclickchooseevent();   //Sự kiện nhấn đúp chọn  //lẽ ra chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu,ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
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
                    options.focusfromdbclickitem = false;   //Không thể kích hoạt vòng lặpfocus
                    return;
                }

                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu,ẩn đi
                }
                if (typeof options.inputtextcheck === 'function') {
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
                return; //Giá trị không thay đổi  thì không kích hoạt
            }

            //Ẩn trước
            if (options.container) {
                options.container.find('ul').html(''); //Xóaulnội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu,ẩn đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //bindSự kiện phím lên/xuống
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li');    //Tất cả các tr trong lớp nổi hiện tại
                if ($trs.length == 0) {
                    return; //Không có dòng
                }
                var curActiveIndex = -1;    //hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0;  //Đíchindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                }
                else {
                    if (event.keyCode == 37) {
                        //alert("Đã nhấn←phím!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("Đã nhấn →phím!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("Đã nhấn↑phím!");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("Đã nhấn↓phím!");
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
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active');    //Tất cả các li trong lớp nổi hiện tại
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //Nhấn vào chỗ khác để ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
    else {
        //Bởiclickkích hoạt bằng sự kiện
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //Xóa nội dung tbody
            }
            $('body div.ui-nfs:visible').hide();

            options.okload();
        });

        //Nhấn vào chỗ khác để ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            if (($target.attr('data-releatednfs') === 'true')) {
                return;
            }
            if ($target.parents('div.ui-nfs').length == 0 && $('body div.ui-nfs:visible').length > 0) {
                $('body div.ui-nfs:visible').hide();
            }
        })
    }
};