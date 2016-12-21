var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var ok = confirm("Bạn chắc chắn muốn xóa?");
            if (ok) {
                var id = btn.data('id');
                $.ajax({
                    url: "/AdminObajuStore/ApplicationUser/Delete",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        if (response.status) {
                            location.reload();
                            console.log("Xóa thành công");
                        }
                    }
                });
            }
        });
    }
}
user.init();