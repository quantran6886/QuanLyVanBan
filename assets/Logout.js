function ShowLogout() {
    swal({
        title: "Đăng xuất tài khoản",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        cancelButtonText: "Đóng [ X ]",
        cancelButtonClass: "btn-sm btn-grd-inverse btn-round",
        confirmButtonClass: "btn-sm btn-round btn-grd-primary",
        confirmButtonText: "Đăng xuất",
    }, function () {
        setTimeout(function () {
            window.open("/Login/Logoff", "_self");
        }, 1000);
    });
}
function LockScreen() {
    $(".lockShow").load("/LockScreen/LockScreen", function () {
        $("#gridSystemModal").modal("show");
    });
}
function UnLockScreen() {
    //var host = window.location.host;
    var pathArray = window.location.pathname.split('/');
    $(".lockShow").load(pathArray[1], function () {
        $("#gridSystemModal").modal("hide");
        $(".modal-backdrop").hide();
    });
}