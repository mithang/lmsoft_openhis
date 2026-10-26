/**
 * jquery.citys.js 1.0
 * http://jquerywidget.com
 */
;(function (factory) {
    if (typeof define === "function" && (define.amd || define.cmd) && !jQuery) {
        // AMDhoặcCMD
        define([ "jquery" ],factory);
    } else if (typeof module === 'object' && module.exports) {
        // Node/CommonJS
        module.exports = function( root, jQuery ) {
            if ( jQuery === undefined ) {
                if ( typeof window !== 'undefined' ) {
                    jQuery = require('jquery');
                } else {
                    jQuery = require('jquery')(root);
                }
            }
            factory(jQuery);
            return jQuery;
        };
    } else {
        //Browser globals
        factory(jQuery);
    }
}(function ($) {
    $.support.cors = true;
    $.fn.citys = function(parameter,getApi) {
        if(typeof parameter == 'function'){ //Tải lại
            getApi = parameter;
            parameter = {};
        }else{
            parameter = parameter || {};
            getApi = getApi||function(){};
        }
        var defaults = {

            dataUrl:'../../Content/js/city.json',     //Cơ sở dữ liệuĐịa chỉ
            dataType:'json',          //Cơ sở dữ liệuLoại:'json'hoặc'jsonp'
            provinceField:'province', //Tên trường tỉnh
            cityField:'city',         //Tên trường thành phố
            areaField:'area',         //Tên trường khu vực
            valueType:'code',         //Giá trị của hộp thả xuốngLoại,codeĐơn vị hành chínhMã,nameTên địa danh
            code:0,                   //Khu vựcMã
            province:0,               //Tỉnh,Có thể là mã vùng hoặc tên
            city:0,                   //Thành phố,Có thể là mã vùng hoặc tên
            area:0,                   //Khu vực,Có thể là mã vùng hoặc tên
            required: true,           //Có bắt buộc phải chọn một hay không
            nodata: 'hidden',         //Khi không có dữ liệugiờhình thức thể hiện của:'hidden'Ẩn,'disabled'Vô hiệu hóa,Để trống thì không xử lý gì
            onChange:function(){}     //Chuyển đổi khu vựcgiờKích hoạt,Hàm gọi lại truyền vào dữ liệu khu vực
        };
        var options = $.extend({}, defaults, parameter);
        return this.each(function() {
            //Định nghĩa đối tượng
            var _api = {};
            var $this = $(this);
            var $province = $this.find('select[name="'+options.provinceField+'"]'),
                $city = $this.find('select[name="'+options.cityField+'"]'),
                $area = $this.find('select[name="'+options.areaField+'"]');
            $.ajax({
                url:options.dataUrl,
                type:'GET',
                crossDomain: true,
                dataType:options.dataType,
                jsonpCallback:'jsonp_location',
                success:function(data){
                    var province,city,area,hasCity;
                    if(options.code){   //Nếu đặt khu vựcMã，thì bỏ qua thông tin đặt riêng lẻ
                        var c = options.code - options.code%1e4;
                        if(data[c]){
                            options.province = c;
                        }
                        c = options.code - (options.code%1e4 ? options.code%1e2 : options.code);
                        if(data[c]){
                            options.city = c;
                        }
                        c = options.code%1e2 ? options.code : 0;
                        if(data[c]){
                            options.area = c;
                        }
                    }
                    var updateData = function(){
                        province = {},city={},area={};
                        hasCity = false;       //Phán đoán có phải thành phố cấp địa hay không
                        for(var code in data){
                            if(!(code%1e4)){     //Lấy tất cả các đơn vị hành chính cấp tỉnh
                                province[code]=data[code];
                                if(options.required&&!options.province){
                                    if(options.city&&!(options.city%1e4)){  //Tỉnh chưa điền, và phán đoán là thành phố trực thuộc trung ương
                                        options.province = options.city;
                                    }else{
                                        options.province = code;
                                    }
                                }else if(data[code].indexOf(options.province)>-1){
                                    options.province = isNaN(options.province)?code:options.province;
                                }
                            }else{
                                var p = code - options.province;
                                if(options.province&&p>0&&p<1e4){    //Thành phố cùng tỉnhhoặcKhu vực
                                    if(!(code%100)){
                                        hasCity = true;
                                        city[code]=data[code];
                                        if(data[code].indexOf(options.city)>-1){
                                            options.city = isNaN(options.city)?code:options.city;
                                        }
                                    }else if(p>9000){                   //Đơn vị hành chính cấp huyện trực thuộc tỉnh
                                        city[code] = data[code];
                                        if(data[code].indexOf(options.city)>-1){
                                            options.city = isNaN(options.city)?code:options.city;
                                        }
                                    }else if(hasCity){                  //Không phải thành phố trực thuộc trung ương
                                        var c = code-options.city;
                                        if(options.city&&c>0&&c<100){     //Khu vực cùng thành phố
                                            area[code]=data[code];
                                            if(data[code].indexOf(options.area)>-1){
                                                options.area = isNaN(options.area)?code:options.area;
                                            }
                                        }
                                    }else{
                                        area[code]=data[code];            //Thành phố trực thuộc trung ương
                                        if(data[code].indexOf(options.area)>-1){
                                            options.area = isNaN(options.area)?code:options.area;
                                        }
                                    }
                                }
                            }
                        }
                    };
                    var format = {
                        province:function(){
                            $province.empty();
                            if(!options.required){
                                $province.append('<option value=""> - Vui lòng chọn - </option>');
                            }
                            for(var i in province){
                                $province.append('<option value="'+(options.valueType=='code'?i:province[i])+'" data-code="'+i+'">'+province[i]+'</option>');
                            }
                            if(options.province){
                                var value = options.valueType=='code'?options.province:province[options.province];
                                $province.val(value);
                            }
                            this.city();
                        },
                        city:function(){
                            $city.empty();
                            if(!hasCity){
                                //$city.css('display','none');
                            }else{
                                $city.css('display','');
                                if(!options.required){
                                    $city.append('<option value=""> - Vui lòng chọn - </option>');
                                }
                                if(options.nodata=='disabled'){
                                    $city.prop('disabled',$.isEmptyObject(city));
                                }else if(options.nodata=='hidden'){
                                    $city.css('display',$.isEmptyObject(city)?'none':'');
                                }
                                for(var i in city){
                                    $city.append('<option value="'+(options.valueType=='code'?i:city[i])+'" data-code="'+i+'">'+city[i]+'</option>');
                                }
                                if(options.city){
                                    var value = options.valueType=='code'?options.city:city[options.city];
                                    $city.val(value);
                                }else if(options.area){
                                    var value = options.valueType=='code'?options.area:city[options.area];
                                    $city.val(value);
                                }
                            }
                            this.area();
                        },
                        area:function(){
                            $area.empty();
                            if(!options.required){
                                $area.append('<option value=""> - Vui lòng chọn - </option>');
                            }
                            if(options.nodata=='disabled'){
                                $area.prop('disabled',$.isEmptyObject(area));
                            }else if(options.nodata=='hidden'){
                                //$area.css('display',$.isEmptyObject(area)?'none':'');
                                $area.css('display',  '');
                            }
                            for(var i in area){
                                $area.append('<option value="'+(options.valueType=='code'?i:area[i])+'" data-code="'+i+'">'+area[i]+'</option>');
                            }
                            if(options.area){
                                var value = options.valueType=='code'?options.area:area[options.area];
                                $area.val(value);
                            }
                        }
                    };
                    //Lấy thông tin địa lý hiện tại
                    _api.getInfo = function(){
                        var status = {
                            direct:!hasCity,
                            province:data[options.province]||'',
                            city:data[options.city]||'',
                            area:data[options.area]||'',
                            code:options.area||options.city||options.province
                        };
                        return status;
                    };
                    //Ràng buộc sự kiện
                    $province.on('change', function () {
                        options.province = $(this).find('option:selected').data('code') ||0; //Mã hành chính của nút đã chọn
                        options.city = 0;
                        options.area = 0;
                        updateData();
                        format.city();
                        options.onChange(_api.getInfo());
                    });
                    $city.on('change',function(){
                        options.city = $(this).find('option:selected').data('code')||0; //Mã hành chính của nút đã chọn
                        options.area = 0;
                        updateData();
                        format.area();
                        options.onChange(_api.getInfo());
                    });
                    $area.on('change',function(){
                        options.area = $(this).find('option:selected').data('code')||0; //Mã hành chính của nút đã chọn
                        options.onChange(_api.getInfo());
                    });
                    //Khởi tạo
                    updateData();
                    format.province();
                    if(options.code){
                        options.onChange(_api.getInfo());
                    }
                    getApi(_api);
                }
            });
        });
    };
}));
