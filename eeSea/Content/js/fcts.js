 function toggleSideBar() {
    // side toggle
    $('div#sideToggleShow').click(
    function () {
        ToggleColumn('tblContent', 'tdCont', 'side', '');
        
        $('div#side').show();
        $('div#sideToggleShow').hide();
        $('div#sideToggleHide').show();
    });

    $('div#sideToggleHide').click(
    function () {
        ToggleColumn('tblContent', 'tdCont', 'side', 'none');
        $('div#side').hide();
        $('div#sideToggleHide').hide();
        $('div#sideToggleShow').show();
    });

    // $('div#side').hide();
    $('#tblContent tr').each(function () {
        $(this).find('td[id*="side"]').css("display", 'none'); //Attribute Contain Selector ([id*=) used for id value.


    });
     
    $('div#sideToggleHide').hide();
    $('div#sideToggleShow').show();

}


function ToggleColumn(tblId,tdCont, tdSide, visState) {
    $('#' + tblId + ' tr').each(function () {
        $(this).find('td[id*="' + tdSide + '"]').css("display", visState); //Attribute Contain Selector ([id*=) used for id value.
        $(this).find('td[id*="' + tdCont + '"]').css("width", '100%');
         
    });

    //ToggleColumn('tblCat', 'tdCatReq', 'hidden');
    //ToggleColumn('tblCat', 'tdCatReq', 'visible'); 
} 

