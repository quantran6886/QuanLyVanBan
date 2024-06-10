var hasReger = {
    init: function () {
        hasReger.LoadMenu();
    },
    LoadMenu: function () {
        $.ajax({
            type: 'Get',
            datatype: 'json',
            url: '/RoleSession/LoadMenu',
            success: function (has) {
                if (has.status == true) {
                    var lstMenu = has.lstMenu;
                    htmlXML = '';
                    var template = $('#data-template-menu-main').html();
                    $.each(lstMenu, function (i, item) {
                        htmlXML += Mustache.render(template, {
                            NameMenu: item.NameMenu,
                            Url: item.Url,
                            NameIcon: item.NameIcon,
                            NameColor: item.NameColor,
                            Controller: item.Controller,
                        });
                    });
                    $('#menuNavBar').html(htmlXML);
                    is_check_menu = false
                }
            }
        })
    },
};
hasReger.init();
