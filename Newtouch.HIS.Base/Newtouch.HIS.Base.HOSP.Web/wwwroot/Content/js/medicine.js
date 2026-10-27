//药品名称
$.fn.ypFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector({
        width: 600,
        height: 300,
        caption: "Tên thuốc",
        url: '/SysMedicine/GetFloatingYPXXJson',
        ajaxparameters: function ($thistr) {
            return 'organizeId=' + $.trim($('#OrganizeId').val()) + '&keyword=' + $thistr;
        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Tên', name: 'ypmc', widthratio: 25 },
            { label: 'Mã BHYT', name: 'ybdm', widthratio: 20 },
            { label: 'Quy cách', name: 'gg', widthratio: 20 },
            { label: 'Tên nhà sản xuất', name: 'ycmc', widthratio: 25 },
            { label: 'Tính chất thuốc', name: 'ypxz', widthratio: 25 },
            { label: 'Pinyin', name: 'py', hidden: true },
            { label: 'Tính chất thuốc', name: 'ybxz', widthratio: 25, hidden: true }
        ]
    });
}

//药品分类
$.fn.ypflFloatingSelector = function (options) {
    $(this).newtouchFloatingSelector( {
        url: '/PharmacyDrugStorage/SysMedicineClassification/GetListSelectJson',
        width: 300,
        height: 300,
        clickautotrigger: true,
        caption: "Phân loại thuốc",
        ajaxparameters: function ($thisinput) {
            return "keyword=" + $thisinput;
        },
        itemdbclickhandler: null,
        colModel: [
            { label: 'Mã', name: 'ypflCode', hidden: true },
            { label: 'Tên', name: 'ypflmc', widthratio: 50 },
            {
                label: 'Mã đầu chữ', name: 'py', widthratio: 50, formatter: function (val) {
                    return $.trim(val);
                }
            }
        ]
    });
}