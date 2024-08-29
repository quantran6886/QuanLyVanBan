function sendMessenger() {
    var smsNoiDung = $("#smsNoiDung").val();
    var smsSoDienThoai = $("#smsSoDienThoai").val();
    if (smsNoiDung == "" || smsNoiDung == undefined || smsNoiDung == null) {
        noti_Info('Chưa có nội dung');
        $("#smsNoiDung").focus();
        return;
    }
    if (smsSoDienThoai == "" || smsSoDienThoai == undefined || smsSoDienThoai == null) {
        noti_Info('Chưa có thông tin liên hệ');
        $("#smsSoDienThoai").focus();
        return;
    }
    if (smsSoDienThoai.length != 10) {
        noti_Info('Số điện thoại không đúng. Vui lòng kiểm tra lại');
        $("#smsSoDienThoai").focus();
        return;
    }
    swal({
        title: "Gửi nội dung?",
        text: "Chúng tôi sẽ liên hệ bạn sớm nhất có thể!",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: "#1ab394",
        confirmButtonText: "Xác nhận",
        cancelButtonColor: "#ed5565",
        cancelButtonText: "Hủy",
        closeOnConfirm: true
    }, function (result) {
        if (result == true) {
            $.ajax({
                url: '/Notification/sendMessenger',
                type: 'POST',
                dataType: 'json',
                data: {
                    smsNoiDung: smsNoiDung,
                    smsSoDienThoai: smsSoDienThoai,
                },
                success: function (response) {
                    if (response.status === true) {
                        document.getElementById("myForm").style.display = "none";
                        $("#smsNoiDung").val("");
                        $("#smsSoDienThoai").val("");
                        noti_Success('Đã gửi thành công');
                    } else {
                        noti_Error(response.message);
                    }
                },
            });
        }
    })
}
function openForm() {
    $("#modal-message-support").modal("show");
}
function openFormZalo() {
    $("#modal-zalo-support").modal("show");
}