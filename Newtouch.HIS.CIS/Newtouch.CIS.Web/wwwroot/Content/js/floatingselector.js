/* newtouch lớp nổibộ chọn */
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
        //CóKhôngSử dụngjsonp
        isinputchangetriggered: true,
        //Cóinput changekích hoạtcủa（soVD phiếunhấpNút lớp nổirađến，thìCótruyềnfalse）
        inputtextcheck: null,
        //doinput changekích hoạt，có thểtrướckiểm tranhậpcủavăn bản function
        mutiselect: false,
        bwlx: null,  //Vị tríLoại
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
        //Ngẫu nhiêntạoid
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //liên kếtlớp nổi
    //tạolớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height+45),
                width: options.width
            });
            //add by sunny 20180807 Tần suấtmỗidòngđốiđủ
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            //if (options.bwlx != "jc")
            $divContainer.append('<div style="height:40px;position:relative"><table class="form"> <tr> <td class="formTitle" style="width:35px">Phân loại:</td><td class="formValue"> <span class="second"><input type="text" name="makeupCo" id="makeupCo" autocomplete="off" style ="width:60px" class="makeinp" placeholder="" /></span> </td> <td class="formTitle" style="width:60px">Vị tríTên:</td><td class="formValue"> <input type = "text"  class= "btn_sr" value = "" style = "width:95px;border: 1px solid #ddd; display:table-cell;" ></td> </tr></table> <select id="bwfl" name="bwfl" class="sel sel_bwdl" size="10" style = "width: 100px;border: 1px solid rgb(153, 153, 153);position: absolute;left: 35px;top: 0px;height: 106px;margin: 31px 0px 0px;outline: none;z-index: 10001;"></select></div> ');
            $divContainer.append('<div class="' + className+'"><ul></ul>');
            //VDnếunhiềuchọn，tăng thêmXác nhậnĐóngNút
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="Xác nhận" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "Đóng" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//gắnnhiềuchọncủaxác nhậnĐóngsự kiện、đỉnhbộĐiều kiện lọcsự kiện
        }
        else {
            options.container = $divContainer;
        }
        //if (options.bwlx != "jc")
            options.funcBindBwflSelData();
    };
    //cholớp nổiđịnh ký tự（TheoelementcủaVị trí）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //Theooptions.$thiscủatọa độ đếnđịnh ký tự
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
                //phía dướikhôngđủdùng,đặtđếntrênphương
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            } else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //tảiDữ liệu
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
            //thông quagiao diệnLấyDữ liệu
            debugger;
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters =  options.ajaxparameters(options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
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
                            //Ngăn lộn xộnhiển thịhiện
                            if (!!options.lastRequestMark && options.lastRequestMark != (thisUrl + (!!thisReqData ? thisReqData.toString() : ''))) {
                                return;
                            }
                            if (options.filter) {
                                data = (options.filter)(data);
                            } (options.funcBindRowData)(data);
                        } else {
                            if (options.container) {
                                options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
                            }
                            //options.container.hide();
                        }
                    },
                    error: function () {
                        options.container.hide();
                    }
                });
            } else {
                //jsonptảiDữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn lộn xộnhiển thịhiện
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
    //Vị tríPhân loại
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
                    options.container.find('.sel_bwdl').html(''); //xóa hiện tạioptionnội dung

                    $.each(data, function (index, el) {
                        options.TempArr[index] = this.bwflmc;
                    });
                    
                } 
            }
        });
    }

    //đượcđếnQuay lạikếtnếu，duyệthiển thị
    options.funcBindRowData = function (resultObjArr) {
        debugger;
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Thuộc tínhGán giá trị
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
    //bindphiếunhấpĐã chọnsự kiện
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li',
                function () {
                    debugger
                    if (options.mutiselect) { //nhiềuchọn
                        if ($(this).hasClass('active')) { //HủyĐã chọn
                            $(this).removeClass('active');
                        } else { //Đã chọn 
                            $(this).addClass('active');
                        };
                    } else //phiếuchọn
                    {
                        debugger
                        options.itemdbclickhandler($(this), options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //VDnếuCóinputbảnthâncủachangekích hoạt chovăn bảnkhungtiêu điểmđiểm
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //nhấp đúpmộtdòng chỗlýxongcủasau
                        }
                        options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
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
            options.itemdbclickhandler(selectorsList, options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
            options.container.find('.btn_close').trigger('click');
        });

        //Đóng
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //VDnếuCóinputbảnthâncủachangekích hoạt chovăn bảnkhungtiêu điểmđiểm
            }
            options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
            options.container.hide();
        });
        options.container.find('.sel_bwdl').change(function () {
            var selval = options.container.find('.sel_bwdl').val();
            options.container.find('.makeinp').val(selval == "0" ? "" : selval);
            options.container.find('.sel_bwdl').css({ "display": "none" });
            options.loaddata();
        });
        options.container.find('.btn_sr').keydown(function (e) {
            if (e.keyCode == 13)//vềxe
            {
                options.loaddata();
            }
        });
        options.container.find('.makeinp').focus(function () {
            options.container.find('.sel_bwdl').html(''); //xóa hiện tạioptionnội dung
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
            options.container.find('.sel_bwdl').html(''); //xóa hiện tạioptionnội dung
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
            options.create(); //tạolớp nổi
        }
        options.location(); //định ký tự
        options.container.show(); //định ký tựtốt lạiHiển thị
        options.loaddata(); //tảiDữ liệu
        options.binddbclickchooseevent(); //nhấp đúpĐã chọnsự kiện  //ứngCókích hoạtmột lầnthìcó thểlấyđã
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
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
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }
                options.okload();
            });
        }

        options.$this.keyup(function (event) { //phảicầnCókeyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }
            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //Giá trịKhông cóđổi  thìkhôngkích hoạt
            }

            //trướcẨn
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạiulnội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //khôngđếnđộ dài tối thiểu，Ẩncủa
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bindphímđĩatrêndướisự kiện
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //hiện tạilớp nổitrongcủatất cảtr
                if ($trs.length == 0) {
                    return; //Không códòng
                }
                var curActiveIndex = -1; //hiện tạiactivecủadòng
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
                        //alert("theođã←phím！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("theođã →phím！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("theođã↑phím！");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("theođã↓phím！");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //gọichỉnhsrollcủaVị trí
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //hiện tạilớp nổitrongcủatất cảli
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //phiếunhấpKhác Ẩnlớp nổi
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
        //doclicksự kiệnđếnkích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //xóa hiện tạitbodynội dung
            }
            $('body div.ui-nfs:visible').hide();
            options.okload();
        });

        //phiếunhấpKhác Ẩnlớp nổi
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

/* Lớp nổi tần suấtbộ chọn */
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
        isjsonp: false,  //CóKhôngSử dụngjsonp
        isinputchangetriggered: true,    //Cóinput changekích hoạtcủa（soVD phiếunhấpNút lớp nổirađến，thìCótruyềnfalse）
        inputtextcheck: null,   //doinput changekích hoạt，có thểtrướckiểm tranhậpcủavăn bản function
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
        //Ngẫu nhiêntạoid
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        console.log(idtemp);
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //liên kếtlớp nổi

    //tạolớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 2),
                width: options.width
            });
            //add by sunny 20180807 Tần suấtmỗidòngđốiđủ
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
    //cholớp nổiđịnh ký tự（TheoelementcủaVị trí）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        }
        else {
            //Theooptions.$thiscủatọa độ đếnđịnh ký tự
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
                //phía dướikhôngđủdùng,đặtđếntrênphương
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            }
            else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //tảiDữ liệu
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
            //thông quagiao diệnLấyDữ liệu
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this);  //aikích hoạtcủa，Quay lạichotrangmặt
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

                            //Ngăn lộn xộnhiển thịhiện
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
                //jsonptảiDữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn lộn xộnhiển thịhiện
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
    //đượcđếnQuay lạikếtnếu，duyệthiển thị
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Thuộc tínhGán giá trị
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
    //bindphiếunhấpĐã chọnsự kiện
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li', function () {
                options.itemdbclickhandler($(this), options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
                if (options.isinputchangetriggered) {
                    //VDnếuCóinputbảnthâncủachangekích hoạt chovăn bảnkhungtiêu điểmđiểm
                    options.focusfromdbclickitem = true;
                    options.$this.trigger('focus');
                }
                if (options.itemdbclickhandleComplete) {
                    options.itemdbclickhandleComplete($(this));    //nhấp đúpmộtdòng chỗlýxongcủasau
                }
                options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
                options.container.hide();
            });
        }
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //tạolớp nổi
        }
        options.location();   //định ký tự
        options.container.show();   //định ký tựtốt lạiHiển thị
        options.loaddata();   //tảiDữ liệu
        options.binddbclickchooseevent();   //nhấp đúpĐã chọnsự kiện  //ứngCókích hoạtmột lầnthìcó thểlấyđã
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
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
                    options.focusfromdbclickitem = false;   //khôngcó thểLặp lạikích hoạtfocus
                    return;
                }

                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }

                options.okload();
            });
        }

        options.$this.keyup(function (event) {   //phảicầnCókeyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }

            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //Giá trịKhông cóđổi  thìkhôngkích hoạt
            }

            //trướcẨn
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạiulnội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //khôngđếnđộ dài tối thiểu，Ẩncủa
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }

            options.okload();
        });

        //bindphímđĩatrêndướisự kiện
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li');    //hiện tạilớp nổitrongcủatất cảtr
                if ($trs.length == 0) {
                    return; //Không códòng
                }
                var curActiveIndex = -1;    //hiện tạiactivecủadòng
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
                        //alert("theođã←phím！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("theođã →phím！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("theođã↑phím！");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("theođã↓phím！");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    }
                    else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //gọichỉnhsrollcủaVị trí
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            }
            else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active');    //hiện tạilớp nổitrongcủatất cảli
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //phiếunhấpKhác Ẩnlớp nổi
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
        //doclicksự kiệnđếnkích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //xóa hiện tạitbodynội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.okload();
        });

        //phiếunhấpKhác Ẩnlớp nổi
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

/* newtouch uốngănchỉhiển thịlớp nổibộ chọn */
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
        //function post hoặc jsonp 
        ajaxsync: false,
        ajaxparameters: null,
        //function get
        isjsonp: false,
        //CóKhôngSử dụngjsonp
        isinputchangetriggered: true,
        //Cóinput changekích hoạtcủa（soVD phiếunhấpNút lớp nổirađến，thìCótruyềnfalse）
        inputtextcheck: null,
        //doinput changekích hoạt，có thểtrướckiểm tranhậpcủavăn bản function
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
        //Ngẫu nhiêntạoid
        var idtemp = Math.random().toString();
        var idtemp = idtemp.replace(/\./, "");
        options.id = "divfloat_" + idtemp;
    }
    if (!options.id) {
        throw new Error("argument id is required");
        return;
    }

    options.$this.attr('data-releatednfs', 'true'); //liên kếtlớp nổi
    //tạolớp nổi
    options.create = function () {
        var $divContainer = $('body').find('div#' + options.id + '');
        if ($divContainer.length == 0) {
            $('body').append('<div id="' + options.id + '" class="ui-nfs" style="position:absolute;display:none;z-index:10001;"></div>');
            var $divContainer = $('body').find('div#' + options.id);
            $divContainer.css({
                height: (options.height + 8),
                width: options.width
            });
            //add by sunny 20180807 Tần suấtmỗidòngđốiđủ
            var className = "ui-nfs-container";
            if (options.className !== undefined) {
                className = options.className
            }
            $divContainer.append('<div class="' + className + '"><ul></ul>');
            //VDnếunhiềuchọn，tăng thêmXác nhậnĐóngNút
            if (options.mutiselect) {
                $divContainer.append('<div class="ui-nfs-container_bottom" style="float: right;width: 95px;margin-top: -5px;"><input type="button" class="btn btn-primary btn_submit"  value="Xác nhận" style="display:table-cell;margin-right: 10px;"><input type = "button"  class= "btn btn-primary btn_close" value = "Đóng" style = "display:table-cell;" ></div>');
            }
            $divContainer.append('</div >');
            if (options.tableWidth) {
                $divContainer.find('ul').css('width', options.tableWidth);
            }
            options.container = $divContainer;
            options.bindclick();//gắnnhiềuchọncủaxác nhậnĐóngsự kiện
        }
        else {
            options.container = $divContainer;
        }

    };
    //cholớp nổiđịnh ký tự（TheoelementcủaVị trí）
    options.location = function () {
        if (options.X && options.Y) {
            options.container.css('left', options.X);
            options.container.css('top', options.Y);
        } else {
            //Theooptions.$thiscủatọa độ đếnđịnh ký tự
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
                //phía dướikhôngđủdùng,đặtđếntrênphương
                options.container.css('top', (thisDomPoint.t - options.height - 3) + "px");
            } else {
                options.container.css('top', (thisDomPoint.t + options.$this.height() + 2) + "px");
            }
        }
    };
    //tảiDữ liệu
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
            //thông quagiao diệnLấyDữ liệu
            var thisUrl = options.url;
            var thisReqData = null;
            if (options.ajaxparameters) {
                var thisParameters = options.ajaxparameters(options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
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
                            //Ngăn lộn xộnhiển thịhiện
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
                //jsonptảiDữ liệu
                $.ajax({
                    url: thisUrl,
                    dataType: 'JSONP',
                    jsonp: 'jsonpCallback',
                    jsonpCallback: "just_callback_in_success",
                    data: thisReqData,
                    success: function (data) {
                        if (data && data.code == "10000" && data.data) {

                            //Ngăn lộn xộnhiển thịhiện
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
    //đượcđếnQuay lạikếtnếu，duyệthiển thị
    options.funcBindRowData = function (resultObjArr) {
        if (resultObjArr) {
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
            }
            if (options.height) {
                options.container.find('ul').css('height', (options.height - 33));
            }
            for (i = 0; i < resultObjArr.length; i++) {
                var liHtml = '<li>' + resultObjArr[i][options.showtext] + "</li>";
                var $li = $(liHtml);
                $.each(options.attrcols, function () {
                    $li.attr('data-' + this, resultObjArr[i][this]);   //data- Thuộc tínhGán giá trị
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
    //bindphiếunhấpĐã chọnsự kiện
    options.binddbclickchooseevent = function () {
        if (options.container && options.itemdbclickhandler) {
            options.container.unbind();
            options.container.on('click', 'ul li',
                function () {
                    if (options.mutiselect) { //nhiềuchọn
                        if ($(this).hasClass('active')) { //HủyĐã chọn
                            $(this).removeClass('active');
                        } else { //Đã chọn 
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
                    } else //phiếuchọn
                    {
                        options.itemdbclickhandler($(this), options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
                        if (options.isinputchangetriggered) {
                            options.$this.trigger('focus'); //VDnếuCóinputbảnthâncủachangekích hoạt chovăn bảnkhungtiêu điểmđiểm
                        }
                        if (options.itemdbclickhandleComplete) {
                            ;
                            options.itemdbclickhandleComplete($(this)); //nhấp đúpmộtdòng chỗlýxongcủasau
                        }
                        options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
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
            options.itemdbclickhandler(selectorsList, options.$this); //aikích hoạtcủa，Quay lạichotrangmặt
            options.container.find('.btn_close').trigger('click');
        });

        //Đóng
        options.container.find('.btn_close').click(function () {
            if (options.isinputchangetriggered) {
                options.$this.trigger('focus'); //VDnếuCóinputbảnthâncủachangekích hoạt chovăn bảnkhungtiêu điểmđiểm
            }
            options.container.find('ul').html(''); //xóa hiện tạitbodynội dung
            options.container.hide();
        });
    }

    options.okload = function () {
        if (!options.container) {
            options.create(); //tạolớp nổi
        }
        options.location(); //định ký tự
        options.container.show(); //định ký tựtốt lạiHiển thị
        options.loaddata(); //tảiDữ liệu
        options.binddbclickchooseevent(); //nhấp đúpĐã chọnsự kiện  //ứngCókích hoạtmột lầnthìcó thểlấyđã
    }

    if (options.isinputchangetriggered) {
        if (options.clickautotrigger === true) {
            options.$this.click(function (event) {
                $('body div.ui-nfs:visible').hide();

                options.prveValue = options.$this.val();
                if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
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
                    return; //khôngđếnđộ dài tối thiểu，Ẩncủa
                }
                if (typeof options.inputtextcheck === 'function') {
                    if (!(options.inputtextcheck(options.$this.val()))) {
                        return;
                    }
                }
                options.okload();
            });
        }

        options.$this.keyup(function (event) { //phảicầnCókeyup
            event = event || window.event;
            if (event.keyCode == 38 || event.keyCode == 40 || event.keyCode == 13) {
                return;
            }
            if (options.$this.val() == options.prveValue && options.$this.val().length == options.prveValue.length) {
                return; //Giá trịKhông cóđổi  thìkhôngkích hoạt
            }

            //trướcẨn
            if (options.container) {
                options.container.find('ul').html(''); //xóa hiện tạiulnội dung
            }
            $('body div.ui-nfs:visible').hide();

            options.prveValue = options.$this.val();
            if (options.minlength && $.trim(options.$this.val()).length < options.minlength) {
                return; //khôngđếnđộ dài tối thiểu，Ẩncủa
            }
            if (typeof options.inputtextcheck === 'function') {
                if (!(options.inputtextcheck(options.$this.val()))) {
                    return;
                }
            }
            options.okload();
        });

        //bindphímđĩatrêndướisự kiện
        options.$this.keydown(function (event) {
            event = event || window.event;
            if (event.keyCode == 37 || event.keyCode == 39 && options.container) {
                var $trs = options.container.find('ul li'); //hiện tạilớp nổitrongcủatất cảtr
                if ($trs.length == 0) {
                    return; //Không códòng
                }
                var curActiveIndex = -1; //hiện tạiactivecủadòng
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
                        //alert("theođã←phím！");
                        dstnIndex = curActiveIndex - 1;
                    }
                    if (event.keyCode == 39) {
                        //alert("theođã →phím！");
                        dstnIndex = curActiveIndex + 1;
                    }
                    //if (event.keyCode == 38) {
                    //    //alert("theođã↑phím！");
                    //    dstnIndex = curActiveIndex - 1;
                    //}
                    //if (event.keyCode == 40) {
                    //    //alert("theođã↓phím！");
                    //    dstnIndex = curActiveIndex + 1;
                    //}
                    if (dstnIndex < 0) {
                        dstnIndex = 0;
                    } else if (dstnIndex > $trs.length - 1) {
                        dstnIndex = 0;
                    }
                }

                //gọichỉnhsrollcủaVị trí
                var scrollTop = $trs[dstnIndex].offsetTop;
                $trs.parents('.ui-nfs tbody').scrollTop(scrollTop - 50);

                $trs.removeClass('active');
                $($trs[dstnIndex]).addClass('active');

                stopDefault(event);
                stopPropagation(event);
            } else if (event.keyCode == 13) {
                if (options.container) {
                    var $activeTrs = options.container.find('ul li.active'); //hiện tạilớp nổitrongcủatất cảli
                    if ($activeTrs.length == 1) {
                        $activeTrs.trigger('click');
                    }
                }
            }
        });

        //phiếunhấpKhác Ẩnlớp nổi
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
        //doclicksự kiệnđếnkích hoạt
        options.$this.click(function () {
            if (options.container) {
                options.container.find('ul li').html(''); //xóa hiện tạitbodynội dung
            }
            $('body div.ui-nfs:visible').hide();
            options.okload();
        });

        //phiếunhấpKhác Ẩnlớp nổi
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



