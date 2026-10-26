$.guianyibaoBase = {
    $yibaoBaseAX: $('#guianyibaoBaseAX')[0],
    /// <summary>
    /// Xử lý dữ liệu hạng mục dịch vụ
    /// <summary>
    DealFwxm: function (psFolderPath, lsKeyWord, jdry) {
        return eval('(' + this.$yibaoBaseAX.DealFwxm(psFolderPath, lsKeyWord, jdry) + ')');
    }
};