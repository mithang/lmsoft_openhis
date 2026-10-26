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
        //function post Hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //Có sử dụng khôngjsonp
        isinputchangetriggered: true,
        //Cóinput changeđược kích hoạt (ví dụ như Nhấn nút lớp nổi xuất hiện, tức là truyềnfalse）
        inputtextcheck: null,
        //bởiinput changeđược kích hoạt, có thể xác thực trước văn bản đã nhập function
        mutiselect: false,
        bwlx: null,  //Loại vị trí
        TempArr:[] 
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
                height: (options.height+45),
                width: options.width
            });
            //add by sunny 20180807 Tần suất căn đều mỗi dòng
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            //if (options.bwlx != "jc")
            $divContainer.append('<div style="height:40px;position:relative"><table class="form"> <tr> <td class="formTitle" style="width:35px">Phân loại:</td><td class="formValue"> <span class="second"><input type="text" name="makeupCo" id="makeupCo" autocomplete="off" style ="width:60px" class="makeinp" placeholder="" /></span> </td> <td class="formTitle" style="width:60px">Tên vị trí:</td><td class="formValue"> <input type = "text"  class= "btn_sr" value = "" style = "width:95px;border: 1px solid #ddd; display:table-cell;" ></td> </tr></table> <select id="bwfl" name="bwfl" class="sel sel_bwdl" size="10" style = "width: 100px;border: 1px solid rgb(153, 153, 153);position: absolute;left: 35px;top: 0px;height: 106px;margin: 31px 0px 0px;outline: none;z-index: 10001;"></select></div> ');
            $divContainer.append('<div class="' + className+'"><ul></ul>');
            //Nếu chọn nhiều, thêm nút xác định để đóng
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="Xác định" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "Đóng" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//Liên kết sự kiện xác nhận đóng chọn nhiều, sự kiện điều kiện lọc trên cùng
        }
        else {
            options.container = $divContainer;
        }
        //if (options.bwlx != "jc")
            options.funcBindBwflSelData();
    };
    //Định vị lớp nổi (dựa theoelementvị trí của)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //Theooptions.$thistọa độ của để định vị
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
                //Phía dưới không đủ dùng,Đặt lên phía trên
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
            debugger;
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters =  options.ajaxparameters(options.$this); //Ai đã kích hoạt, trả về cho trang
                if (thisParameters) {
                    thisUrl += "?" + thisParameters;
                }
                if (options.bwlx == "jc")
                    thisUrl += "?keyword=";
                //if (options.bwlx != "jc")
                //{
                    var bwfl = options.container.find('.sel_bwdl').val();
                    var bwmc = options.container.find('.btn_sr').val();
                    if (bwfl && bwfl != 0)
                    {
                        thisUrl += "&bwfl=" + bwfl;
                    }
                    if (bwmc) {
                        thisUrl += "&bwmc=" + bwmc;
                    }
               // }
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
                            if (options.container) {
                                options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
                            }
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
    //Phân loại vị trí
    options.funcBindBwflSelData = function () {
        var bwurl = "/SystemManage/BodyParts/GetSysBodyBwFl";
        if (options.bwlx == "jc")
            bwurl = "/SystemManage/BodyParts/GetSysBodyJcBwFl";
        $.ajax({
            type: options.ajaxmethod,
            url: bwurl,
            data:null,
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data && data.length && data.length > 0) {
                    options.container.find('.sel_bwdl').html(''); //Xóa hiện tạioptionNội dung

                    $.each(data, function (index, el) {
                        options.TempArr[index] = this.bwflmc;
                    });
                    
                } 
            }
        });
    }

    //Nhận kết quả trả về, duyệt và hiển thị nó
    options.funcBindRowData = function (resultObjArr) {
        debugger;
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Gán giá trị thuộc tính
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
                    debugger
                    if (options.mutiselect) { //Chọn nhiều
                        if ($(this).hasClass('active')) { //Hủy chọn
                            $(this).removeClass('active');
                        } else { //Chọn 
                            $(this).addClass('active');
                        };
                    } else //Chọn một
                    {
                        debugger
                        options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt, trả về cho trang
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //Nếu làinputcủa chính nóchangeKích hoạt Đặt tiêu điểm cho hộp văn bản
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //Nhấn đúp một dòng Sau khi xử lý xong
                        }
                        options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
                        options.container.hide();
                    }
                });
        }
    }

    options.bindclick = function () {
        //Xác định 
        options.container.find('.btn_submit').click(function () {
            selectorsList = [];
            options.container.find('li').each(function () {
                if ($(this).hasClass('active')) {
                    selectorsList.push($(this));
                }
            });
            options.itemdbclickhandler(selectorsList, options.$this); //Ai đã kích hoạt, trả về cho trang
            options.container.find('.btn_close').trigger('click');
        });

        //Đóng
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //Nếu làinputcủa chính nóchangeKích hoạt Đặt tiêu điểm cho hộp văn bản
            }
            options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
            options.container.hide();
        });
        options.container.find('.sel_bwdl').change(function () {
            var selval = options.container.find('.sel_bwdl').val();
            options.container.find('.makeinp').val(selval == "0" ? "" : selval);
            options.container.find('.sel_bwdl').css({ "display": "none" });
            options.loaddata();
        });
        options.container.find('.btn_sr').keydown(function (e) {
            if (e.keyCode == 13)//Enter
            {
                options.loaddata();
            }
        });
        options.container.find('.makeinp').focus(function () {
            options.container.find('.sel_bwdl').html(''); //Xóa hiện tạioptionNội dung
            options.container.find('.sel_bwdl').css({
                "display": "",
            });
            var option = "<option value='0'>Tất cả</option>";
            var $op = $(option);
            $op.appendTo(options.container.find('.sel_bwdl'));
            for (i = 0; i < options.TempArr.length; i++) {
                var option = "<option value='" + options.TempArr[i] + "'>" + options.TempArr[i] + "</option>";
                var $op = $(option);
                $op.appendTo(options.container.find('.sel_bwdl'));
            }
        });
        options.container.find('.makeinp').on('input',function () {
            options.container.find('.sel_bwdl').html(''); //Xóa hiện tạioptionNội dung
            var makeval = options.container.find('.makeinp').val();
            var option = "<option value='0'>Tất cả</option>";
            var $op = $(option);
            $op.appendTo(options.container.find('.sel_bwdl'));
            for (i = 0; i < options.TempArr.length; i++) {
                if (options.TempArr[i].substring(0, makeval.length).indexOf(makeval) == 0) {
                    var option = "<option value='" + options.TempArr[i] + "'>" + options.TempArr[i] + "</option>";
                    var $op = $(option);
                    $op.appendTo(options.container.find('.sel_bwdl'));
                }
                
            }
        });
    }
    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location(); //Định vị
        options.container.show(); //Định vị xong rồi hiển thị
        options.loaddata(); //Tải dữ liệu
        options.binddbclickchooseevent(); //Sự kiện nhấn đúp chọn  //Có lẽ chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                options.container.find('ul').html(''); //Xóa hiện tạiulNội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bindSự kiện lên xuống bàn phím
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //Tất cả trong lớp nổi hiện tạitr
                if ($trs.length == 0) {
                    return; //Không có dòng
                }
                var curActiveIndex = -1; //Hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0; //Mục tiêuindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                } else {
                    if (event.keyCode == 37) {
                        //alert("đã nhấn←phím!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("đã nhấn →phím!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("đã nhấn↑phím!");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("đã nhấn↓phím!");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnhsrollvị trí của
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //Tất cả trong lớp nổi hiện tạili
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //Nhấn vào mục khác Ẩn lớp nổi
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
        //bởiclicksự kiện để kích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //Xóa hiện tạitbodyNội dung
            }
            $('body div.ui-nfs:visible').hide();
            options.okload();
        });

        //Nhấn vào mục khác Ẩn lớp nổi
        $(document).click(function (e) {
            e = e || window.event;
            $target = $(e.target);
            var elem = e.target || e.srcElement;
            if (elem.id != 'bwfl' && elem.id != "makeupCo") {
                if (options.container)
                    options.container.find('.sel_bwdl').css({ "display": "none" });
               
            }

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
        ajaxreqdata: null,  //function post Hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,    //function get
        isjsonp: false,  //Có sử dụng khôngjsonp
        isinputchangetriggered: true,    //Cóinput changeđược kích hoạt (ví dụ như Nhấn nút lớp nổi xuất hiện, tức là truyềnfalse）
        inputtextcheck: null,   //bởiinput changeđược kích hoạt, có thể xác thực trước văn bản đã nhập function
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
            //add by sunny 20180807 Tần suất căn đều mỗi dòng
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
    //Định vị lớp nổi (dựa theoelementvị trí của)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        }
        else {
            //Theooptions.$thistọa độ của để định vị
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
                //Phía dưới không đủ dùng,Đặt lên phía trên
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
                var thisParameters = options.ajaxparameters(options.$this);  //Ai đã kích hoạt, trả về cho trang
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
    //Nhận kết quả trả về, duyệt và hiển thị nó
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Gán giá trị thuộc tính
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
                options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt, trả về cho trang
                if (options.isinputchangetriggered) {
                    //Nếu làinputcủa chính nóchangeKích hoạt Đặt tiêu điểm cho hộp văn bản
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //Nhấn đúp một dòng Sau khi xử lý xong
                }
                options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //Tạo lớp nổi
        }
        options.location();   //Định vị
        options.container.show();   //Định vị xong rồi hiển thị
        options.loaddata();   //Tải dữ liệu
        options.binddbclickchooseevent();   //Sự kiện nhấn đúp chọn  //Có lẽ chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                    options.focusfromdbclickitem = false;   //Không thể kích hoạt theo vòng lặpfocus
                    return;
                }

                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                options.container.find('ul').html(''); //Xóa hiện tạiulNội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //bindSự kiện lên xuống bàn phím
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li');    //Tất cả trong lớp nổi hiện tạitr
                if ($trs.length == 0) {
                    return; //Không có dòng
                }
                var curActiveIndex = -1;    //Hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0;  //Mục tiêuindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                }
                else {
                    if (event.keyCode == 37) {
                        //alert("đã nhấn←phím!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("đã nhấn →phím!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("đã nhấn↑phím!");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("đã nhấn↓phím!");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    }
                    else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnhsrollvị trí của
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active');    //Tất cả trong lớp nổi hiện tạili
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //Nhấn vào mục khác Ẩn lớp nổi
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
        //bởiclicksự kiện để kích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //Xóa hiện tạitbodyNội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.okload();
        });

        //Nhấn vào mục khác Ẩn lớp nổi
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

/* newtouch Bộ chọn lớp nổi chỉ định ăn uống */
$.fn.multiFoodFloatingSelector = function (options, data) {
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
        //function post Hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //Có sử dụng khôngjsonp
        isinputchangetriggered: true,
        //Cóinput changeđược kích hoạt (ví dụ như Nhấn nút lớp nổi xuất hiện, tức là truyềnfalse）
        inputtextcheck: null,
        //bởiinput changeđược kích hoạt, có thể xác thực trước văn bản đã nhập function
        mutiselect: false
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
            //add by sunny 20180807 Tần suất căn đều mỗi dòng
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className + '"><ul></ul>');
            //Nếu chọn nhiều, thêm nút xác định để đóng
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="Xác định" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "Đóng" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//Liên kết sự kiện xác nhận đóng chọn nhiều
        }
        else {
            options.container = $divContainer;
        }

    };
    //Định vị lớp nổi (dựa theoelementvị trí của)
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //Theooptions.$thistọa độ của để định vị
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
                //Phía dưới không đủ dùng,Đặt lên phía trên
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
                var thisParameters = options.ajaxparameters(options.$this); //Ai đã kích hoạt, trả về cho trang
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
    //Nhận kết quả trả về, duyệt và hiển thị nó
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Gán giá trị thuộc tính
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
                        } else { //Chọn 
                            var $this = $(this);
                            var $thiszh = $this.attr('data-DietGroup');
                            options.container.find('li').each(function () {
                                var $thisgroup = $(this).attr('data-DietGroup');
                                if ($(this).hasClass('active') && $thisgroup === $thiszh) {
                                    $(this).removeClass('active');
                                }
                            });
                            $(this).addClass('active');
                        };
                    } else //Chọn một
                    {
                        options.itemdbclickhandler($(this), options.$this); //Ai đã kích hoạt, trả về cho trang
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //Nếu làinputcủa chính nóchangeKích hoạt Đặt tiêu điểm cho hộp văn bản
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //Nhấn đúp một dòng Sau khi xử lý xong
                        }
                        options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
                        options.container.hide();
                    }
                });
        }
    }

    options.bindclick = function () {
        //Xác định 
        options.container.find('.btn_submit').click(function () {
            selectorsList = [];
            options.container.find('li').each(function () {
                if ($(this).hasClass('active')) {
                    selectorsList.push($(this));
                }
            });
            options.itemdbclickhandler(selectorsList, options.$this); //Ai đã kích hoạt, trả về cho trang
            options.container.find('.btn_close').trigger('click');
        });

        //Đóng
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //Nếu làinputcủa chính nóchangeKích hoạt Đặt tiêu điểm cho hộp văn bản
            }
            options.container.find('ul').html(''); //Xóa hiện tạitbodyNội dung
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
        options.binddbclickchooseevent(); //Sự kiện nhấn đúp chọn  //Có lẽ chỉ cần kích hoạt một lần là được
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                    return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
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
                options.container.find('ul').html(''); //Xóa hiện tạiulNội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //Chưa đạt độ dài tối thiểu, ẩn nó đi
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bindSự kiện lên xuống bàn phím
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //Tất cả trong lớp nổi hiện tạitr
                if ($trs.length == 0) {
                    return; //Không có dòng
                }
                var curActiveIndex = -1; //Hiện tạiactivedòng
                for (iiiiii = 0; iiiiii < $trs.length; iiiiii++) {
                    if ($($trs[iiiiii]).hasClass('active')) {
                        curActiveIndex = iiiiii;
                    }
                }
                var dstnIndex = 0; //Mục tiêuindex
                if (curActiveIndex == -1) {
                    dstnIndex = 0;
                } else {
                    if (event.keyCode == 37) {
                        //alert("đã nhấn←phím!");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("đã nhấn →phím!");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("đã nhấn↑phím!");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("đã nhấn↓phím!");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //Điều chỉnhsrollvị trí của
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //Tất cả trong lớp nổi hiện tạili
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //Nhấn vào mục khác Ẩn lớp nổi
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
        //bởiclicksự kiện để kích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //Xóa hiện tạitbodyNội dung
            }
            $('body div.ui-nfs:visible').hide();
            options.okload();
        });

        //Nhấn vào mục khác Ẩn lớp nổi
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



