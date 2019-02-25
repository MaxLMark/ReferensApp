$(".hour").click(function (e) {
    //e.preventDefault();
    var $this = $(this);
    var bookingid = $(this).data("bookingid");
    if (bookingid == undefined) {
        bookingid = 0;
    }

    var monthPicked = $(this).data("month");
    var dayPicked = $(this).data("day");
    var hourPicked = $(this).data("hour");
    var bookedby = $(this).data("bookedby");

    if (bookedby == undefined || bookedby == "") {

        $("#basicModal").data("hourPicked", hourPicked).modal("toggle", $this);
        $("#basicModal").data("monthPicked", monthPicked);
        $("#basicModal").data("dayPicked", dayPicked);
        $("#basicModal").data("bookingid", bookingid);

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
    $("#bookingid").val($(this).data("bookingid"));

    if ($(this).data("bookingid") == 0) {
        $("#closeBookingBtn").hide();
        $("#openBookingBtn").show();
    } else {
        $("#closeBookingBtn").show();
        $("#openBookingBtn").hide();
    }

});

$("#cancelBookingModal").on("shown.bs.modal", function (e) {

    $('.bookedBy').text("Tiden är bokad av: " + $(this).data("bookedby"));

    console.log($(this).data("bookingid"));
    $("#bookingId").val($(this).data("bookingid"));
})