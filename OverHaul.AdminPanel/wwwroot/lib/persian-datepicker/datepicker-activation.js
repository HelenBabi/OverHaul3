$(document).ready(function () {
    // فعال‌سازی PersianDatepicker برای تمام ورودی‌ها با کلاس persian-datepicker
    $('.persian-datepicker').persianDatepicker({
        format: 'YYYY/MM/DD',
        autoClose: true,
        toolbox: {
            calendarSwitch: {
                enabled: false
            }
        }
    });
});