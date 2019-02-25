$(".hour").click(function (e) {
    //e.preventDefault();
    var $this = $(this);
    var bookingid = $(this).data("bookingid");
    var monthPicked = $(this).data("month");
    var dayPicked = $(this).data("day");
    var hourPicked = $(this).data("hour");
    var bookedby = $(this).data("bookedby");

    if (bookedby == "") {

        $("#basicModal").data("hourPicked", hourPicked).modal("toggle", $this);
        $("#basicModal").data("monthPicked", monthPicked);
        $("#basicModal").data("dayPicked", dayPicked);

    } else {

        $("#cancelBookingModal").data("bookedby", bookedby).modal("toggle", $this);
        $("#cancelBookingModal").data("bookingid", bookingid);
    }
});

$("#basicModal").on("shown.bs.modal", function (e) {
    if ($(this).data("hourPicked") < 10) {
        $('.bookedTime').text("Du har valt följande tid: 0" + $(this).data("hourPicked") + ":00");
    } else {
        $('.bookedTime').text("Du har valt följande tid: " + $(this).data("hourPicked") + ":00");
    }

    $("#monthPicked").val($(this).data("monthPicked"));
    $("#dayPicked").val($(this).data("dayPicked"));
    $("#hourPicked").val($(this).data("hourPicked"));
});

$("#cancelBookingModal").on("shown.bs.modal", function (e) {

    $('.bookedBy').text("Tiden är bokad av: " + $(this).data("bookedby"));
    $("#bookingId").val($(this).data("bookingid"));
})