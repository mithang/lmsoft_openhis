/**
 * jquery.citys.js 1.0
 * http://jquerywidget.com
 */
; (function (factory) {
	if (typeof define === "" && (define.amd || define.cmd) && !jQuery) {
		// AMDHoặcCMD
		define([""], factory);
	} else if (typeof module === '' && module.exports) {
		// Node/CommonJS
		module.exports = function (root, jQuery) {
			if (jQuery === undefined) {
				if (typeof window !== '') {
					jQuery = require('');
				} else {
					jQuery = require('')(root);
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
	$.fn.citys = function (parameter, getApi) {
		if (typeof parameter == '') { //重载
			getApi = parameter;
			parameter = {};
		} else {
			parameter = parameter || {};
			getApi = getApi || function () { };
		}
		var defaults = {

			dataUrl: '',     //Địa chỉ cơ sở dữ liệu
			dataType: '',          //Loại cơ sở dữ liệu:''Hoặc''
			provinceField: '', //省份字段名
			cityField: '',         //城市字段名
			areaField: '',         //地区字段名
			valueType: '',         //下拉框值的类型,code行政区划代码,name地名
			code: 0,                   //Mã quận
			province: 0,               //Phần tỉnh,可以为地区编码或者名称
			city: 0,                   //城市,可以为地区编码或者名称
			area: 0,                   //Quận,可以为地区编码或者名称
			required: true,           //是否必须选一个
			nodata: '',         //当无数据时的表现形式:''Ẩn,''禁用,为空不做任何处理
			onChange: function () { }     //地区切换时触发,回调函数传入地区数据
		};
		var options = $.extend({}, defaults, parameter);
		return this.each(function () {
			//对象定义
			var _api = {};
			var $this = $(this);
			var $province = $this.find('' + options.provinceField + ''),
                $city = $this.find('' + options.cityField + ''),
                $area = $this.find('' + options.areaField + '');
			$.ajax({
				url: options.dataUrl,
				type: '',
				crossDomain: true,
				dataType: options.dataType,
				jsonpCallback: '',
				success: function (data) {
					var province, city, area, hasCity;
					if (options.code) {   //Mã quận đặt nếu，则忽略单独设置的信息
						var c = options.code - options.code % 1e4;
						if (data[c]) {
							options.province = c;
						}
						c = options.code - (options.code % 1e4 ? options.code % 1e2 : options.code);
						if (data[c]) {
							options.city = c;
						}
						c = options.code % 1e2 ? options.code : 0;
						if (data[c]) {
							options.area = c;
						}
					}
					var updateData = function () {
						province = {}, city = {}, area = {};
						hasCity = false;       //判断是非有地级城市
						for (var code in data) {
							if (!(code % 1e4)) {     //获取所有的省级行政单位
								province[code] = data[code];
								if (options.required && !options.province) {
									if (options.city && !(options.city % 1e4)) {  //省未填，并判断为直辖市
										options.province = options.city;
									} else {
										options.province = code;
									}
								} else if (data[code].indexOf(options.province) > -1) {
									options.province = isNaN(options.province) ? code : options.province;
								}
							} else {
								var p = code - options.province;
								if (options.province && p > 0 && p < 1e4) {    //同省的城市或地区
									if (!(code % 100)) {
										hasCity = true;
										city[code] = data[code];
										if (data[code].indexOf(options.city) > -1) {
											options.city = isNaN(options.city) ? code : options.city;
										}
									} else if (p > 9000) {                   //省直辖县级行政单位
										city[code] = data[code];
										if (data[code].indexOf(options.city) > -1) {
											options.city = isNaN(options.city) ? code : options.city;
										}
									} else if (hasCity) {                  //非直辖市
										var c = code - options.city;
										if (options.city && c > 0 && c < 100) {     //同个城市的地区
											area[code] = data[code];
											if (data[code].indexOf(options.area) > -1) {
												options.area = isNaN(options.area) ? code : options.area;
											}
										}
									} else {
										area[code] = data[code];            //直辖市
										if (data[code].indexOf(options.area) > -1) {
											options.area = isNaN(options.area) ? code : options.area;
										}
									}
								}
							}
						}
					};
					var format = {
						province: function () {
							$province.empty();
							if (!options.required) {
								$province.append('');
							}
							for (var i in province) {
								$province.append('' + (options.valueType == '' ? i : province[i]) + '' + i + '' + province[i] + '');
							}
							if (options.province) {
								var value = options.valueType == '' ? options.province : province[options.province];
								$province.val(value);
							}
							this.city();
						},
						city: function () {
							$city.empty();
							if (!hasCity) {
								//$city.css('','');
							} else {
								$city.css('', '');
								if (!options.required) {
									$city.append('');
								}
								if (options.nodata == '') {
									$city.prop('', $.isEmptyObject(city));
								} else if (options.nodata == '') {
									$city.css('', $.isEmptyObject(city) ? '' : '');
								}
								for (var i in city) {
									$city.append('' + (options.valueType == '' ? i : city[i]) + '' + i + '' + city[i] + '');
								}
								if (options.city) {
									var value = options.valueType == '' ? options.city : city[options.city];
									$city.val(value);
								} else if (options.area) {
									var value = options.valueType == '' ? options.area : city[options.area];
									$city.val(value);
								}
							}
							this.area();
						},
						area: function () {
							$area.empty();
							if (!options.required) {
								$area.append('');
							}
							if (options.nodata == '') {
								$area.prop('', $.isEmptyObject(area));
							} else if (options.nodata == '') {
								//$area.css('',$.isEmptyObject(area)?'':'');
								$area.css('', '');
							}
							for (var i in area) {
								$area.append('' + (options.valueType == '' ? i : area[i]) + '' + i + '' + area[i] + '');
							}
							if (options.area) {
								var value = options.valueType == '' ? options.area : area[options.area];
								$area.val(value);
							}
						}
					};
					//获取当前地理信息
					_api.getInfo = function () {
						var status = {
							direct: !hasCity,
							province: data[options.province] || '',
							city: data[options.city] || '',
							area: data[options.area] || '',
							code: options.area || options.city || options.province
						};
						return status;
					};
					//Sự kiện liên kết
					$province.on('', function () {
						options.province = $(this).find('').data('') || 0; //选中节点的区划代码
                        options.city = 0;
						options.area = 0;
						updateData();
						format.city();
                        options.onChange(_api.getInfo());
                        $city.trigger('');
					});
					$city.on('', function () {
						options.city = $(this).find('').data('') || 0; //选中节点的区划代码
						options.area = 0;
						updateData();
						format.area();
						options.onChange(_api.getInfo());
					});
					$area.on('', function () {
						options.area = $(this).find('').data('') || 0; //选中节点的区划代码
						options.onChange(_api.getInfo());
					});
					//Khởi tạo
					updateData();
					format.province();
					if (options.code) {
						options.onChange(_api.getInfo());
					}
					getApi(_api);
				}
			});
		});
	};
}));
