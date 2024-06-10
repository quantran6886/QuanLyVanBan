var myMenu = [
    {
        icon: 'fa fa-home text-info',
        label: 'Tin tức trang chủ',
        action: function (option, contextMenuIndex, optionIndex) {
            window.open('/t-r-a-n-g-c-h-u', '_self');
        },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-user text-danger',
        label: 'Thông tin tài khoản',
        action: function (option, contextMenuIndex, optionIndex) {
            window.open('/dang-ky-tai-khoan', '_self');
        },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-envelope text-primary',
        label: 'Trung tâm tin nhắn',
        action: function (option, contextMenuIndex, optionIndex) { },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-search',
        label: 'Chức năng tra cứu',
        action: function (option, contextMenuIndex, optionIndex) { },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-cog text-warning',
        label: 'Cấu hình hệ thống',
        action: function (option, contextMenuIndex, optionIndex) { },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-eye text-success',
        label: 'Lịch sử đăng nhập',
        action: function (option, contextMenuIndex, optionIndex) { },
        submenu: null,
        disabled: false
    },
    {
        icon: 'fa fa-lock text-primary',
        label: 'Khóa màn hình',
        action: function (option, contextMenuIndex, optionIndex) {
            $(".lockShow").load("/LockScreen/LockScreen", function () {
                $("#gridSystemModal").modal("show");
            });
        },
        submenu: null,
        disabled: false
    },
];


$('#context-menu-simple').off('contextmenu').on('contextmenu', function (e) {
    superCm.createMenu(myMenu, e);
    $("#LockScreenFull").addClass('modal-backdrop fade show');
    $("#LockScreenFull").css('z-index', 99999999);
    //$(".lockShow").css("transition", "opacity .15s linear;");
    e.preventDefault();
});

$(window).off('click').on('click', function () {
    $("#LockScreenFull").removeClass('modal-backdrop fade show');
    //$(".lockShow").css("transition", "opacity .15s linear;");
});


//$(this).on('click', function (e) {
//    $(".lockShow").css("opacity", "1");
//    $(".lockShow").css("transition", "opacity .15s linear;");
//});