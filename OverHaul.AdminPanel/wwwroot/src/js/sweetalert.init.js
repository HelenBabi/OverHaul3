


//Basic
document.getElementById("sa-basic").addEventListener("click", function() {
    Swal.fire(
        {
            title: 'فرد غیر متخصص میتونه از کامپیوتر استفاده کنه؟',
            confirmButtonColor: '#5156be',
        }
    )
});

//A title with a text under
document.getElementById("sa-title").addEventListener("click", function() {
    Swal.fire(
        {
            title: "اینترنت خوبه؟",
            text: 'مشکلی در برقراری ارتباط دارید؟',
            icon: 'question',
            confirmButtonColor: '#5156be'
        }
    )
});

//Success Message
document.getElementById("sa-success").addEventListener("click", function() {
    Swal.fire(
        {
            title: 'وقت بخیر!',
            text: 'شما کلیک کردید!',
            icon: 'success',
            showCancelButton: true,
			
            confirmButtonText: 'درسته',
            cancelButtonText: 'خیر',
        }
    )
});

//Warning Message
document.getElementById("sa-warning").addEventListener("click", function() {
    Swal.fire({
        title: "شما مطمئن هستید؟",
        text: "نمیتوانید به حالت قبل برگردید!!",
        icon: "warning",
        showCancelButton: true,
       
        cancelButtonText: "لغو",
        confirmButtonText: 'بله حذف کن!',
      }).then(function (result) {
        if (result.value) {
          Swal.fire("حذف شد!", "فایل شما حذف شد.", "success"
          );
        }
    });
});

//Parameter
document.getElementById("sa-params").addEventListener("click", function() {
    Swal.fire({
        title: "شما مطمئن هستید؟",
        text: "نمیتوانید به حالت قبل برگردید!!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'بله حذف کن!',
		
        cancelButtonText: 'نه لغو کن!',
        confirmButtonClass: 'btn btn-success mt-2 ms-2',
        cancelButtonClass: 'btn btn-danger ms-2 mt-2 ml-2 mr-3',
        buttonsStyling: false
    }).then(function (result) {
        if (result.value) {
            Swal.fire({
              title: 'حذف شد!',
              text: 'فایل شما حذف شد.',
              icon: 'success',
              confirmButtonText: 'اوکی',
            })
          } else if (
            // Read more about handling dismissals
            result.dismiss === Swal.DismissReason.cancel
          ) {
            Swal.fire({
              title: 'لغو شد',
              text: 'حذف فایل شما لغو شد :)',
              icon: 'error',
              confirmButtonText: 'اوکی',
            })
          }
    });
});


//Custom Image
document.getElementById("sa-image").addEventListener("click", function() {
    Swal.fire({
        title: 'هشدار!',
        text: 'مدال با یک تصویر.',
        imageUrl: 'images/logo-sm.svg',
        imageHeight: 48,
        confirmButtonColor: "#5156be",
        animation: false
    })
});

//Auto Close Timer
document.getElementById("sa-close").addEventListener("click", function() {
    var timerInterval;
    Swal.fire({
    title: 'بستن اتومات الرت!',
    html: 'من الرت رو میبندم در <b></b> 2ثانیه.',
    timer: 2000,
    timerProgressBar: true,
    didOpen:function () {
        Swal.showLoading()
        timerInterval = setInterval(function() {
        var content = Swal.getHtmlContainer()
        if (content) {
            var b = content.querySelector('b')
            if (b) {
            b.textContent = Swal.getTimerLeft()
            }
        }
        }, 100)
    },
    onClose: function () {
        clearInterval(timerInterval)
    }
    }).then(function (result) {
    /* Read more about handling dismissals below */
    if (result.dismiss === Swal.DismissReason.timer) {
        console.log('I was closed by the timer')
    }
    })
});

//custom html alert
document.getElementById("custom-html-alert").addEventListener("click", function() {
    Swal.fire({
        title: '<i>HTML</i> <u>مثال</u>',
        icon: 'info',
        html: 'میتوانید استفاده کنید از <b>متن بولد</b>, ' +
        '<a href="//Pichforest.in/">لینک ها</a> ' +
        'یا تگ های دیگر HTML ',
        showCloseButton: true,
        showCancelButton: true,
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger ml-1',
        confirmButtonColor: "#47bd9a",
        cancelButtonColor: "#fd625e",
        confirmButtonText: '<i class="si-heart si"></i> تشکر!',
        cancelButtonText: '<i class="si-like si"></i> حله'
    })
});

//position
document.getElementById("sa-position").addEventListener("click", function() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'عملیات شما انجام شد',
        showConfirmButton: false,
        timer: 1500
    })
});

//Custom width padding
document.getElementById("custom-padding-width-alert").addEventListener("click", function() {
    Swal.fire({
        title: 'اندازه و حاشیه سفارشی.',
        width: 600,
        padding: 100,
        confirmButtonColor: "#5156be",
        background: '#e0e1f3'
    })
});

//Ajax
document.getElementById("ajax-alert").addEventListener("click", function() {
    Swal.fire({
        title: 'ایمیل را وارد کنید تا درخواست ارسال شود',
        input: 'email',
        showCancelButton: true,
        confirmButtonText: 'ارسال',
        showLoaderOnConfirm: true,
        confirmButtonColor: "#5156be",
        cancelButtonText: 'لغو',
        preConfirm: function (email) {
            return new Promise(function (resolve, reject) {
                setTimeout(function () {
                    if (email === 'taken@example.com') {
                        reject('این ایمیل از قبل وجود دارد.')
                    } else {
                        resolve()
                    }
                }, 2000)
            })
        },
        allowOutsideClick: false
    }).then(function (email) {
        Swal.fire({
            icon: 'success',
            title: 'درخواست ایجکس شما ارسال شد!',
            confirmButtonText: 'حله',
            html: 'ایمیل ارسال شد: ' + email
        })
    })
});
