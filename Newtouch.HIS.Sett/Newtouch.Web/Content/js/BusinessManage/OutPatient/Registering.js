$(function () {
    registeringListNew([]);
});

function registeringListNew(mydata) {
    var $registeringList = $("");
    $registeringList.dataNewGrid({  
        height: 142,
        colModel: [
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', hidden: true, width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
            { label: '', name: '', width: 100, algin: '' },
        ]
    }, mydata);
    if (mydata.length > 0)
    {
        $('').find("").remove();
    }


}