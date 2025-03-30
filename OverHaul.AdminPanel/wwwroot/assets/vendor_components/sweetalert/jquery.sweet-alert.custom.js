! function($) {
    "use strict";

    var SweetAlert = function() {};

    //examples 
    SweetAlert.prototype.init = function() {

            //Basic
            $('#sa-basic').click(function() {
                swal("Here's a message!");
            });

            //A title with a text under
            $('#sa-title').click(function() {
                swal("Here's a message!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.")
            });

            //Success Message
            $('#sa-success').click(function() {
                swal("Good job!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.", "success")
            });

            //Warning Message
            $('#sa-warning').click(function() {
                swal({
                    title: "شما مطمئن هستید؟",
                    text: "قادر به بازیابی فایل نخواهید بود!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "بله حذفش کن!",
					cancelButtonText: "نه لغو کن!",
                    closeOnConfirm: false
                }, function() {
                    swal("حذف شد!", "فایل شما حذف شد.", "success");
                });
            });

            //Parameter
            $('#sa-params').click(function() {
                swal({
                    title: "شما مطمئن هستید؟",
                    text: "قادر به بازیابی فایل نخواهید بود!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "بله حذف کن!",
                    cancelButtonText: "نه لغو کن!",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function(isConfirm) {
                    if (isConfirm) {
                        swal("حذف شد!", "فایل شما حذف شد.", "success");
                    } else {
                        swal("لغو شد", "حذف فایل شما لغو شد :)", "error");
                    }
                });
            });

            //Custom Image
            $('#sa-image').click(function() {
                swal({
                    title: "Govinda!",
                    text: "Recently joined twitter",
                    imageUrl: "../images/avatar/avatar-1.png"
                });
            });

            //Auto Close Timer
            $('#sa-close').click(function() {
                swal({
                    title: "Auto close alert!",
                    text: "I will close in 2 seconds.",
                    timer: 2000,
                    showConfirmButton: false
                });
            });


        },
        //init
        $.SweetAlert = new SweetAlert, $.SweetAlert.Constructor = SweetAlert
}(window.jQuery),

//initializing 
function($) {
    "use strict";
    $.SweetAlert.init()
}(window.jQuery);