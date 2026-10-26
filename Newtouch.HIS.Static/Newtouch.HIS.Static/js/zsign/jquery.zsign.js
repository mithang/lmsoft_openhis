/*
    desc:jQuery mô phỏng đóng dấu
    author:hyf
    date:2012-11-08
*/

;$.fn.zSign = function (options) {
    var _s = $.extend({
        img: '',
        width: 150,
        height: 150,
        offset: 30,           //Giá trị biên
        callBack: null
    }, options || {});

    var _parent = $(this).addClass('zsign');
    var range = {
        minX: _s.offset,
        minY: _s.offset,
        maxX: $(window).width(),      //Trừ đi 2 padding = 8px và 2 viền 1px
        maxY: $(window).height()
    };

    var _btnPanel = $("<div class='panels'><button class='btn btn-default add ' >Đóng dấu</button><button class='btn btn-default cancel'>Đóng</button></div>").appendTo(_parent);
    var _html = "<div class='sign' style='height:" + _s.height + "px;width:" + _s.width + "px;top:" + _s.offset + "px;left:" + _s.offset + "px'><img src='" + _s.img + "' draggable='false'/><button class='btn btn-default ok' >Xác nhận</button><button class='btn btn-default del' >Xóa</button></div>";


    var _add = $('.add', _btnPanel).click(function (e) {
        _add.attr('disabled', 'disabled');
        var sign = $(_html).appendTo(_parent);
        $('.ok', sign).click(function () {
            //Xác nhận đóng dấu
            sign.addClass('ok').off('mousedown').find('.btn').remove();
            _add.removeAttr('disabled');
            if (_s.callBack) {
                _s.callBack.call(this, { img: _s.img, top: parseInt(sign.css('top')), left: parseInt(sign.css('left')) });
            }
        });
        $('.del', sign).click(function () {
            //Hủy đóng dấu
            sign.remove();
            _add.removeAttr('disabled');
        });

        //Gắn sự kiện di chuyển
        sign.on('mousedown', function (e) {
            sign.data('x', e.clientX);
            sign.data('y', e.clientY);
            var position = sign.position();
            $(document).on('mousemove', function (e1) {
                var x = e1.clientX - sign.data('x') + position.left;
                var y = e1.clientY - sign.data('y') + position.top;
                x = x < range.minX ? range.minX : x;
                x = x > range.maxX ? range.maxX : x;
                y = y < range.minY ? range.minY : y;
                y = y > range.maxY ? range.maxY : y;

                sign.css({ left: x, top: y });
            }).on('mouseup', function () {
                $(this).off('mousemove').off('mouseup');
            });
        });
    });

	$('.cancel', _btnPanel).click(function () {
        var r = true;
        if (_add.attr('disabled') == 'disabled') {
            if (!confirm("Dấu chưa xác định sẽ bị hủy, bạn có chắc chắn muốn đóng không?")) {
                r = false;
            }
        }
        if (r) {
            //Xóa dấu chưa xác định vị trí
            $('.sign:not(.ok)', _parent).remove();
            _btnPanel.remove();
        }
    });
};