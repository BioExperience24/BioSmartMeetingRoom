times = generateTimes(moment().startOf('day'), 15);

let claims = {};

const templateBookingListOld = `
  <li id="list-[[BOOKING_ID]]" class="media" data-meeting-id="[[BOOKING_ID]]">
      <img
      class="mr-3 rounded"
      width="55"
      src="[[IMAGE]]"
      alt="[[ROOM_NAME]]"
      />
      <div class="media-body">
        <div class="float-right">
            [[STATUS]]
        </div>
        <div class="media-title">[[BOOKING_NAME]]</div>
        <div class="text-muted text-small">
            <strong>[[BOOKING_DATE]]</strong> -
            <strong>[[BOOKING_TIME]]</strong>
        </div>
        <div class="mt-1">
            <button
            class="btn btn-primary btn-sm view-details"
            data-meeting-id="[[BOOKING_ID]]"
            >
            View Details
            </button>
        </div>
      </div>
  </li>
`;

const templateBookingList = `
  <div id="list-[[BOOKING_ID]]" class="meeting-card" data-meeting-id="[[BOOKING_ID]]">
    <h6>[[BOOKING_NAME]]</h6>
    [[STATUS]]
    <div class="meeting-content">
      <img src="[[IMAGE]]" alt="[[ROOM_NAME]]" class="meeting-image" />
      <div class="meeting-info">
        <div class="meeting-date">
          <i class="fa fa-calendar"></i> [[BOOKING_DATE]] <br>
          <i class="fa fa-clock"></i> [[BOOKING_TIME]]
        </div>
        <button class="view-details" data-meeting-id="[[BOOKING_ID]]">View Details</button>
      </div>
    </div>
  </div>
`;

const listStatus = [
  { id: "", name: "All" },
  { id: "pending", name: "Pending" },
  { id: "queue", name: "Meeting queue" },
  { id: "ongoing", name: "Ongoing / Meeting in progress" },
  { id: "expired", name: "Expired / Meeting Expired" },
  { id: "canceled", name: "Meeting Canceled" },
  { id: "rejected", name: "Meeting Rejected" },
];

const buttonDetail = `
  <button class="btn btn-danger flex-fill mr-2 py-3" id="cancel-meeting" onclick="cancelMeeting()">
      Cancel Meeting
  </button>
  <button class="btn btn-warning flex-fill py-3" data-toggle="modal" data-target="#rescheduleModal">
      Reschedule
  </button>
`;

const buttonDetailTrainingRoom = `
  <button class="btn btn-danger flex-fill mr-2 py-3" id="cancel-meeting" onclick="cancelMeetingTraining()">
      Cancel Meeting
  </button>
  <button class="btn btn-warning flex-fill py-3" data-toggle="modal" data-target="#rescheduleModal">
      Reschedule
  </button>
`;

const buttonDetailRescheduleOnly = `
  <button class="btn btn-warning flex-fill py-3 w-100" data-toggle="modal" data-target="#rescheduleModal">
      Reschedule
  </button>
`;

const lengthPerLoad = 10;
let page = 1;
let totalPage = 0;
let isLoading = false;

// Meeting data
let meetingData = { };

let ajaxFetchAvailableTimeBooking = false;

$(function () {
  loadDatePicker();
  loadTimePicker();
  loadStatusBooking();
  loadInit();
});

async function loadInit()
{
  try {
    await loadClaims();
  } finally {
    await loadBookingList();
  }
}

async function loadClaims() {
  try {
    var data = await $.ajax({
        type: "Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: bsApiSmr + ajax.url.get_claims,
    });

    claims = {
      role: data["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      nik: data["http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata"],
    }
  } catch (err) {
    console.log(err);
    window.location.href = `/webview/error/401`;
  }
}

async function loadBookingList() {
  let param = {
    start: lengthPerLoad * (page - 1),
    length: lengthPerLoad,
    booking_date: $("#dateRange").val(),
    status: $("#statusFilter").val(), 
  };
  try {
    var data = await $.ajax({
        type: "Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: bsApiSmr + ajax.url.get_datatable_booking,
        data: param,
        beforeSend: function() {
          $("#loader-list").show();
        },
    });

    if (data.status == "success")
    {
      let collection = data.collection;
      // console.log(collection.data);
      $.each(collection.data, function (index, item) {
        let template = templateBookingList
          .replace(/\[\[BOOKING_ID\]\]/g, item.booking_id)
          .replace(/\[\[IMAGE\]\]/g, bsApi + "Room/GetRoomDetailView/" + 
            item.room_image)
          .replace(/\[\[ROOM_NAME\]\]/g, item.room_name)
          .replace(/\[\[STATUS\]\]/g, getStatusBooking(item))
          .replace(/\[\[BOOKING_NAME\]\]/g, item.title)
          .replace(/\[\[BOOKING_DATE\]\]/g, item.booking_date)
          .replace(/\[\[BOOKING_TIME\]\]/g, item.time);

        
        // $("#meeting-list").find("ul").append(template);
        $("#meeting-list").find("div.card-container").append(template);
        $("#list-" + item.booking_id).data(item);
      });
      page += 1;
      totalPage = Math.ceil(collection.recordsTotal / 10);
      isLoading = false;
      $("#loader-list").hide();
    } else {
      console.log("success tapi error");
    }
  } catch (err) {
    $("#loader-list").hide();
    console.log(err);
  }
}

async function refreshBookingList() {
  // $("#meeting-list").find("ul").empty();
  $("#meeting-list").find("div.card-container").empty();
  page = 1;
  totalPage = 0;
  await loadBookingList();
}

function getStatusBooking(item, retObj = false) {
  let statusObj = {};
  let status = "";
  let momentEnd = moment(item.server_end);
  let momentStart = moment(item.server_start);
  let extendTime = item.extended_duration -0;
  let diffMomentEnd = momentEnd.add(extendTime, 'minutes');
  
  // <span class="badge badge-success">Active</span>
  if (item.is_approve == 1) {
      if (moment().unix() > momentEnd.unix()) {
          status = "<span class='badge badge-danger mb-2'>Expired</span>"
          statusObj = {
            text: "Expired",
            class: "badge-danger",
          };
      } else if (
          moment().unix() <= momentEnd.unix() &&
          moment().unix() >= momentStart.unix()
      ) {
          status = "<span class='badge badge-success mb-2'>Meeting in progress</span>"; // meeting dimulai
          statusObj = {
            text: "Meeting in progress",
            class: "badge-success",
          };
      } 
      else if (
          momentStart.diff(moment(), 'minutes') <= 0
      ) {
          status = "<span class='badge badge-primary mb-2'>Ongoing</span>"; // antrian
          statusObj = {
            text: "Ongoing",
            class: "badge-primary",
          };
      }
      else if (
          moment().unix() <= moment(item.start).unix()
      ) {
          status = "<span class='badge badge-info mb-2'>Meeting queue</span>"; // antrian
          statusObj = {
            text: "Meeting queue",
            class: "badge-info",
          };
      }
      else if (
          moment().diff(diffMomentEnd) >= 0
      ) {
          status = "<span class='badge badge-danger mb-2'>Meeting Expired</span>";
          statusObj = {
            text: "Meeting Expired",
            class: "badge-danger",
          };
      }
  } else {
      if (moment().unix() > momentEnd.unix()) {
          status = "<span class='badge badge-danger mb-2'>Expired</span>"
          statusObj = {
            text: "Expired",
            class: "badge-danger",
          };
      } else if (
          moment().diff(diffMomentEnd) >= 0
      ) {
          status = "<span class='badge badge-danger mb-2'>Meeting Expired</span>";
          statusObj = {
            text: "Meeting Expired",
            class: "badge-danger",
          };
      } else  {
          status = "<span class='badge badge-secondary mb-2'>Pending</span>"; // antrian
          statusObj = {
            text: "Pending",
            class: "badge-secondary",
          };
        }
      }
      
  if ((item.is_alive-0)  == 0) {
    status = "<span class='badge badge-secondary mb-2'>Pending</span>"; // antrian
    statusObj = {
      status: "Pending",
      class: "badge-secondary",
    };
  }

  if (item.is_approve == 2) {
      status = "<span class='badge badge-danger mb-2'>Meeting Rejected</span>"; // antrian
      statusObj = {
        status: "Meeting Rejected",
        class: "badge-danger",
      };
  }

  if (item.is_canceled == 1) {
      status = `<span class='badge badge-danger mb-2'>Meeting Canceled</span>`; // antrian
      statusObj = {
        status: "Meeting Canceled",
        class: "badge-danger",
      };
  }

  if (item.is_expired == 1 || item.end_early_meeting == 1) {
      status = `<span class='badge badge-danger mb-2'>Meeting Expired</span>`; // antrian
      statusObj = {
        status: "Meeting Expired",
        class: "badge-danger",
      };
  }

  if (retObj == true) {
    return statusObj;
  }
  return status;
}

function loadDatePicker() {
  // Inisialisasi DateRangePicker untuk dashboard
  $("#dateRange").daterangepicker(
    {
      opens: "right",
      locale: {
        format: "YYYY-MM-DD",
        separator: " - ",
        applyLabel: "Apply",
        cancelLabel: "Cancel",
        customRangeLabel: "Custom",
        daysOfWeek: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        monthNames: [
          "January",
          "February",
          "March",
          "April",
          "May",
          "June",
          "July",
          "August",
          "September",
          "October",
          "November",
          "December",
        ],
        firstDay: 1,
      },
      ranges: {
        Today: [moment(), moment()],
        Yesterday: [
          moment().subtract(1, "days"),
          moment().subtract(1, "days"),
        ],
        "Last 7 Days": [moment().subtract(6, "days"), moment()],
        "Last 30 Days": [moment().subtract(29, "days"), moment()],
        "This Month": [
          moment().startOf("month"),
          moment().endOf("month"),
        ],
        "Last Month": [
          moment().subtract(1, "month").startOf("month"),
          moment().subtract(1, "month").endOf("month"),
        ],
      },
      startDate: moment().startOf("day"),
      endDate: moment().endOf("day"),
    },
    function (start, end, label) {
      setTimeout(() => {
        refreshBookingList();
      }, 100);
    }
  );

  $("#newDate").daterangepicker(
    {
      singleDatePicker: true,
      showDropdowns: true,
      minDate: moment(),
      drops: "auto",
      locale: {
          format: 'YYYY-MM-DD'
      },
      isInvalidDate: function(date) {
        return date.day() === 0 || date.day() === 6;
      }
    }, 
    function(start, end, label) {
    // Ensure the format is consistent
    $("#newDate").val(start.format('YYYY-MM-DD'));
  });

  $("#newDate").on('apply.daterangepicker', function(ev, picker) {
      // console.log("Selected date: " + picker.startDate.format('YYYY-MM-DD'));
      let date = picker.startDate.format('YYYY-MM-DD');
      let rTime = meetingData.raw_time;
      let item = {
        date: date,
        time_start: rTime.time_start,
        time_end: rTime.time_end
      }
      $("#newStartTime, #newEndTime").empty().append(`<option>Loading</option>`).select2();
      fetchAvailableTimeBooking(meetingData.booking_id, item, meetingData.room_id);

      /* let isDisablePastTime = moment(date, "YYYY-MM-DD").isAfter(moment(), "day") ? false : true;
      let startTime = "";
      let endTime = "";
      if (meetingData && Object.keys(meetingData).length !== 0) {
        let item = meetingData.raw_time;
        startTime = moment(date, "YYYY-MM-DD").isSame(moment(item.date, "YYYY-MM-DD"), "day") ? item.time_start : "";
        endTime = moment(date, "YYYY-MM-DD").isSame(moment(item.date, "YYYY-MM-DD"), "day") ? item.time_end : "";
      }
      generateTimeOptions("#newStartTime", startTime, [], isDisablePastTime);
      generateTimeOptions("#newEndTime", endTime, [], isDisablePastTime); */
  });
}

function loadTimePicker() {
  // Inisialisasi Timepicker dengan interval 5 menit
  $("#newStartTime").timepicker({
    timeFormat: "H:i", // Format 24 jam (contoh: 09:00)
    step: 5, // Loncat 5 menit
    minTime: "00:00", // Batas bawah
    maxTime: "23:55", // Batas atas
    dynamic: false,
    dropdown: true,
    scrollbar: true,
  });
  
  $("#newEndTime").timepicker({
    timeFormat: "H:i",
    step: 5, // Loncat 5 menit
    minTime: "00:00",
    maxTime: "23:55",
    dynamic: false,
    dropdown: true,
    scrollbar: true,
  });
}

function loadStatusBooking() {
  $("#statusFilter").empty();
  $.each(listStatus, function (_, item) { 
    $("#statusFilter").append(
      `<option value="${item.id}">${item.name}</option>`
    );
  });
}

// Kembali ke list
function returnToList() {
  $("#meeting-details-section").hide();
  $("#meeting-list-section").show();
  $("#navbar-back").hide();
}

function generateBookingDetail() {
  const data = meetingData;

  $("#meeting-title").text(data.title);
  $("#meeting-status")
    .text(data.status.text)
    .removeClass()
    .addClass(
      `badge p-2 ${data.status.class}`
    );
  $("#meeting-date-time").html(
    `<strong>${data.date}</strong> - <strong>${data.time}</strong>`
  );
  $("#meeting-host").text(data.host);
  $("#meeting-description").text(data.description);
  $("#meeting-attendees").text(data.attendees);

  // Internal attendees
  $("#internal-attendees").html(`
    <ul class="list-group list-group-flush">
      ${data.internal
        .map(
          (att) => `
        <li class="list-group-item d-flex align-items-center justify-content-between attendee-item" data-name="${
          att.name
        }" data-role="${
            att.role
          }" data-toggle="modal" data-target="#attendeeModal">
          <div class="d-flex align-items-center">
            <img src="${window.location.origin}/assets/images/avatar-person.png" class="rounded-circle mr-3" width="40" height="40" alt="${
              att.name
            }" />
            <div>
              <span class="d-block font-weight-bold">${att.name}</span>
              <small class="text-muted">${att.role}</small>
            </div>
          </div>
          <div>
            <i class="fas fa-check-circle text-success"></i>
            ${
              att.isHost
                ? '<i class="fas fa-crown text-warning"></i>'
                : ""
            }
          </div>
        </li>
      `
        )
        .join("")}
    </ul>
  `);

  // External attendees
  $("#external-attendees").html(`
    <ul class="list-group list-group-flush">
      ${
        data.external.length > 0
          ? data.external
              .map(
                (att) => `
        <li class="list-group-item d-flex align-items-center justify-content-between attendee-item" data-name="${att.name}" data-role="${att.role}" data-toggle="modal" data-target="#attendeeModal">
          <div class="d-flex align-items-center">
            <img src="${window.location.origin}/assets/images/avatar-person.png" class="rounded-circle mr-3" width="40" height="40" alt="${att.name}" />
            <div>
              <span class="d-block font-weight-bold">${att.name}</span>
              <small class="text-muted">${att.role}</small>
            </div>
          </div>
          <i class="fas fa-circle text-muted"></i>
        </li>
      `
              )
              .join("")
          : '<li class="list-group-item">No external attendees</li>'
      }
    </ul>
  `);

  $("#meeting-list-section").hide();
  $("#meeting-details-section").show();

  $("#navbar-back").show();
}

function generateBookingDetailButton(item) {
  $("#meeting-details-section").data("detail", item);

  let momentEnd = moment(item.server_end);
  let momentStart = moment(item.server_start);
  let extendTime = item.extended_duration -0;
  let diffMomentEnd = momentEnd.add(extendTime, 'minutes');
  let bookExpr = false;
  
  if ( moment().diff(diffMomentEnd) >= 0 || item.is_expired == 1 ) {
      bookExpr = true;
  }

  let bookRun = false;
    if (item.end_early_meeting == 1 || (
      moment().unix() <= momentEnd.unix() &&
      moment().unix() >= momentStart.unix()
  )) {
      bookRun = true;
  }

  $("#section-button").find("div").empty();
  if (item.booking_type == "trainingroom") {
    if (!isHPlus(item.date, 14)) {
      $("#section-button").find("div").html(buttonDetailRescheduleOnly);
    } else {
      $("#section-button").find("div").html(buttonDetailTrainingRoom);
    }
  } else {
    $("#section-button").find("div").html(buttonDetail);
  }

  $("#section-button").hide();
  $("#end-meeting").hide();
  const isAuthorized = 
    claims.role != "2" || 
    claims.nik == item.created_by || 
    (claims.role == "2" && claims.nik == item.pic_nik);

  if (!bookExpr && item.is_canceled == 0) {
    if (bookRun) {
      if (!item.end_early_meeting && item.is_approve == 1 && isAuthorized) {
        $("#end-meeting").show();
      }
    } else if (isAuthorized) {
      $("#section-button").show();
    }
  }
}

function resetBookingDetail() {
  $("#meeting-title").text("");
  $("#meeting-status")
    .text("")
    .removeClass();
  $("#meeting-date-time").html("");
  $("#meeting-host").text("");
  $("#meeting-description").text("");
  $("#meeting-attendees").text("");

  // Internal attendees
  $("#internal-attendees").empty();

  // External attendees
  $("#external-attendees").empty();

  $("#section-button").hide();
  $("#end-meeting").hide();
  $("#meeting-details-section").removeData("detail");
}

// Cancel meeting dengan SweetAlert
function cancelMeeting() {
  var item = $("#meeting-details-section").data("detail");
  if (!item) {
    Swal.fire({
      title: "Data not found!",
      type: "error",
    }).then(() => {
        $("#navbar-back").trigger("click");
    });
  }

  var id = item.id;
  var booking_id = item.booking_id;
  var name = item.title;
  var form = new FormData();
  form.append('id', id);
  form.append('booking_id', booking_id);
  form.append('name', name);
  Swal.fire({
    title: 'Are you sure want cancel ' + name + ' of meeting?',
    text: "You will cancel the data booking " + name + " !",
    type: "warning",
    showCancelButton: true,
    // confirmButtonColor: '#3085d6',
    // cancelButtonColor: '#d33',
    confirmButtonText: 'Cancel Meeting !',
    cancelButtonText: 'Close !',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
        Swal.fire({
            title: 'Reason for Cancellation',
            input: "text",
            inputAttributes: {
                autocapitalize: "off",
            },
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Submit',
            cancelButtonText: 'Close !',
            reverseButtons: true,
            preConfirm: (result) => {
                if (result == "" || result == null) {
                    return Swal.showValidationMessage(`Reason for Cancellation is required`);
                }
            }
        }).then((result) => {
            if (result.value !== undefined) {
                var reason = result.value;
                form.append('reason', reason);
                $.ajax({
                    url: bsApiSmr + ajax.url.post_cancel_booking,
                    type: "POST",
                    data: form,
                    processData: false,
                    contentType: false,
                    dataType: "json",
                    beforeSend: function() {
                        Swal.fire({
                            title: 'Please Wait !',
                            html: 'Process to cancel meeting',
                            allowOutsideClick: false,
                            onBeforeOpen: () => {
                                Swal.showLoading()
                            },
                        });
                    },
                    success: function(data) {
                        Swal.close();
                        if (data.status == "success") {
                          Swal.fire({
                            title: "Succes cancel " + name,
                            type: "success",
                          });
                        } else {
                          Swal.fire({
                            title: "Cancel " + name + " meeting is failed!!!",
                            type: "error",
                          });
                        }
                        $("#navbar-back").trigger("click");
                    },
                    error: function(err) {
                      Swal.fire({
                        title: "Cancel " + name + " meeting is failed!!!",
                        type: "error",
                      });
                      $("#navbar-back").trigger("click");
                    },
                });
            }
        });
    } 
  });
}

// Cancel meeting training room
function cancelMeetingTraining() {
  var item = $("#meeting-details-section").data("detail");
  if (!item) {
    Swal.fire({
      title: "Data not found!",
      type: "error",
    }).then(() => {
        $("#navbar-back").trigger("click");
    });
  }

  var id = item.id;
  var booking_id = item.booking_id;
  var name = item.title;
  var form = new FormData();
  form.append('id', id);
  form.append('booking_id', booking_id);
  form.append('name', name);

  Swal.fire({
      title: 'Are you sure want cancel ' + name + ' of meeting?',
      text: "You will cancel the data booking " + name + " !",
      type: "warning",
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Cancel Meeting !',
      cancelButtonText: 'Close !',
      reverseButtons: true
  }).then((result) => {
    if (result.value) {
      Swal.fire({
          type: "warning",
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#3085d6',
          confirmButtonText: 'Just This Meeting !',
          cancelButtonText: 'Cancel All Meeting !!',
          reverseButtons: true
      }).then((result) => {
        if (result.value == undefined) {
          form.append('is_all', true);
        } else {
          form.append('is_all', false);
        }
        Swal.fire({
          title: 'Reason for Cancellation',
          input: "text",
          inputAttributes: {
              autocapitalize: "off",
          },
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Submit',
          cancelButtonText: 'Close !',
          reverseButtons: true,
          preConfirm: (result) => {
              if (result == "" || result == null) {
                  return Swal.showValidationMessage(`Reason for Cancellation is required`);
              }
          }
        }).then((result) => {
          if (result.value !== undefined) {
            var reason = result.value;
            form.append('reason', reason);

            $.ajax({
                url: bsApiSmr + ajax.url.post_cancel_all_booking,
                type: "POST",
                data: form,
                processData: false,
                contentType: false,
                dataType: "json",
                beforeSend: function() {
                    $('#id_loader').html('<div class="linePreloader"></div>');
                    Swal.fire({
                        title: 'Please Wait !',
                        html: 'Process to cancel meeting',
                        allowOutsideClick: false,
                        onBeforeOpen: () => {
                            Swal.showLoading()
                        },
                    });
                },
                success: function(data) {
                    Swal.close();
                    if (data.status == "success") {
                      Swal.fire({
                        title: "Succes cancel " + name,
                        type: "success",
                      });
                    } else {
                      Swal.fire({
                        title: "Cancel " + name + " meeting is failed!!!",
                        type: "error",
                      });
                    }
                    $("#navbar-back").trigger("click");
                },
                error: function(err) {
                  Swal.fire({
                    title: "Cancel " + name + " meeting is failed!!!",
                    type: "error",
                  });
                  $("#navbar-back").trigger("click");
                },
            });
          }
        });
      });
    }
  });
}

// End meeting dengan SweetAlert
function endMeeting() {
  var item = $("#meeting-details-section").data("detail");
  if (!item) {
    Swal.fire({
      title: "Data not found!",
      type: "error",
    }).then(() => {
        $("#navbar-back").trigger("click");
    });
  }

  var id = item.id;
  var booking_id = item.booking_id;
  var name = item.title;
  var form = new FormData();
  form.append('id', id);
  form.append('booking_id', booking_id);
  form.append('name', name);
  form.append('user', false);
  
  Swal.fire({
    title: 'Are you sure want end ' + name + ' of meeting?',
    text: "You will end the data booking " + name + " !",
    type: "warning",
    showCancelButton: true,
    // confirmButtonColor: '#3085d6',
    // cancelButtonColor: '#d33',
    confirmButtonText: 'End Meeting !',
    cancelButtonText: 'Close !',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
        $.ajax({
            url: bsApiSmr + ajax.url.post_end_meeting,
            type: "POST",
            data: form,
            processData: false,
            contentType: false,
            dataType: "json",
            beforeSend: function() {
                Swal.fire({
                    title: 'Please Wait !',
                    html: 'Process to end meeting',
                    allowOutsideClick: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    },
                });
            },
            success: function(data) {
                Swal.close();
                if (data.status == "success") {
                  Swal.fire({
                    title: "Succes end " + name,
                    type: "success",
                  });
                } else {
                  Swal.fire({
                    title: "End " + name + " meeting is failed!!!",
                    type: "error",
                  });
                }
                $("#navbar-back").trigger("click");
            },
            error: function(err) {
              Swal.fire({
                title: "End " + name + " meeting is failed!!!",
                type: "error",
              });
              $("#navbar-back").trigger("click");
            },
        })
    } else {

    }
  });
}

async function fetchAvailableTimeBooking(bookingId, rawTime, roomId) {
  try {
    ajaxFetchAvailableTimeBooking = true;
    var data = await $.ajax({
        type: "Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: bsApiSmr + ajax.url.get_check_available_time + `/${bookingId}/${rawTime.date}/${roomId}`,
    });
    ajaxFetchAvailableTimeBooking = false;
    if (data.status == "success") {
      let collection = data.collection[0];
      let disabledTimes = collection.datatime
                          .filter(item => item.book === 1) // Ambil item dengan book = 1
                          .map(item => item.time_array); // Ambil time_array dari item yang sudah difilter
      disabledTimes = disabledTimes.map(time => moment(time, "HH:mm:ss").format("HH:mm"));
      
      let isDisablePastTime = moment(rawTime.date, "YYYY-MM-DD").isAfter(moment(), "day") ? false : true;

      // generateTimeOptions("#newStartTime", rawTime.time_start, disabledTimes, isDisablePastTime);
      // generateTimeOptions("#newEndTime", rawTime.time_end, disabledTimes, isDisablePastTime);

      let startTime = "";
      let endTime = "";
      if (meetingData && Object.keys(meetingData).length !== 0) {
        let rTime = meetingData.raw_time;
        startTime = moment(rawTime.date, "YYYY-MM-DD").isSame(moment(rTime.date, "YYYY-MM-DD"), "day") ? rTime.time_start : "";
        endTime = moment(rawTime.date, "YYYY-MM-DD").isSame(moment(rTime.date, "YYYY-MM-DD"), "day") ? rTime.time_end : "";
      }

      generateTimeOptions("#newStartTime", startTime, disabledTimes, isDisablePastTime);
      generateTimeOptions("#newEndTime", endTime, disabledTimes, isDisablePastTime);
    } else {
      showErrorLoadNotification();
    }

  } catch (err) {
    ajaxFetchAvailableTimeBooking = false;
    showErrorLoadNotification();
  }
}

$("#statusFilter").on("change", function () {
  setTimeout(() => {
    refreshBookingList();
  }, 100);
});

$(window).on('scroll', async function () {
  if (isLoading) return;

  const scrollTop = $(window).scrollTop();
  const windowHeight = $(window).height();
  const documentHeight = $(document).height();

  // Jika scroll hampir sampai bawah (misalnya sisa 100px)
  if (scrollTop + windowHeight + 100 >= documentHeight) {
      if (page <= totalPage) {
        isLoading = true;
        await loadBookingList();
      }
  }
});

// View details click
$(document).on("click", ".view-details", function () {
  // const row = $(this).parents("li");
  const row = $(this).parents("div.meeting-card");
  const item = row.data();

  let internal = [];
  $.each(item.attendees_list.internal_attendess, function (_, uin) {
    internal.push({
      name: uin.name,
      role: uin.position,
      isHost: uin.nik == item.pic_nik ? true : false,
    });
  });

  let external = [];
  $.each(item.attendees_list.external_attendess, function (_, uex) {
    external.push({
      name: uex.name,
      role: uex.position,
    });
  });

  meetingData = {
    title: `${item.title} / ${item.building_name} / ${item.room_name}`,
    status: getStatusBooking(item, true),
    date: item.booking_date,
    time: item.time,
    host: item.pic,
    description: item.note,
    attendees: item.attendees_list.internal_attendess.length + item.attendees_list.external_attendess.length + " Persons",
    internal: internal,
    external: external,
    booking_id: item.booking_id,
    room_id: item.room_id,
    raw_time: {
      date: item.date,
      time_start: moment(item.start).format("HH:mm"),
      time_end: moment(item.end).format("HH:mm"),
    }
  }

  generateBookingDetail();
  generateBookingDetailButton(item);
});

// Tombol back
$("#navbar-back").click(async function (e) {
  e.preventDefault();
  if ($("#meeting-details-section").is(":visible")) {
    meetingData = {};
    resetBookingDetail();
    returnToList();
    await refreshBookingList();
  }
});

// Attendees Modal Logic
$(document).on("click", ".attendee-item", function () {
  var name = $(this).data("name");
  var role = $(this).data("role");
  $("#attendeeName").text(name);
  $("#attendeeRole").text(role);
});

// Reschedule modal logic
// Logika untuk tombol tipe meeting di modal
$(".meeting-type-btn").on("click", function (e) {
  e.preventDefault();
  const meetingType = $(this).data("type");
  
  let query = {
    token: token,
    type: meetingType,
  };

  // console.log($.param(query));

  window.location.href = window.location.origin + "/webview/booking/find-room?"+$.param(query);
});

$('#rescheduleModal').on('shown.bs.modal', function (e) {
  if (meetingData && Object.keys(meetingData).length === 0) {
    $("#navbar-back").trigger("click");
  }

  let item = meetingData.raw_time;

  let date = $('#newDate').data('daterangepicker');
  $('#newDate').val(item.date);
  date.setStartDate(item.date);
  date.setEndDate(item.date);

  // let isDisablePastTime = moment(item.date, "YYYY-MM-DD").isAfter(moment(), "day") ? false : true;
  // generateTimeOptions("#newStartTime", item.time_start, [], isDisablePastTime);
  // generateTimeOptions("#newEndTime", item.time_end, [], isDisablePastTime);
  fetchAvailableTimeBooking(meetingData.booking_id, item, meetingData.room_id);
});

$('#rescheduleModal').on('hidden.bs.modal', function (e) {
  $("#newStartTime, #newEndTime").empty().append(`<option>Loading</option>`).select2();
});

$("#saveReschedule").on("click", function (e) {
  $('#frm_reschedule').trigger("submit");
});

$('#frm_reschedule').submit(function(e) {
  e.preventDefault();

  if (ajaxFetchAvailableTimeBooking) {
    return false;
  }

  var form = $('#frm_reschedule').serializeArray();
  var unixTimeStart = moment($('#newStartTime').val(), "HH:mm").format('X');
  var unixTimeEnd = moment($('#newEndTime').val(), "HH:mm").format('X');


  // console.log($('#id_frm_res_start_input').val())
  if ($('#newDate').val() == "") {
      Swal.fire('Attention !!!', 'Date coloumn cannot be empty!', 'warning')
      return false;
  } else if ($('#newStartTime').val() == "" || $('#newEndTime').val() == "") {
      Swal.fire('Attention !!!', 'Please selected the time!', 'warning')
      return false;
  } else if (unixTimeStart > unixTimeEnd || unixTimeStart == unixTimeEnd) {
      Swal.fire('Attention !!!', 'Start time must to be more than Finish time, or start & finish time cannot be equal!', 'warning')
      return false;
  }

  if (meetingData && Object.keys(meetingData).length === 0 && meetingData.booking_id == "") {
    Swal.fire('Attention !!!', 'Meeting Data not found!', 'warning')
    return false;
  }

  form = form.map(item => {
    if (item.name === "start" || item.name === "end") {
        item.value = `${$("#newDate").val()} ${item.value}`;
    }
    return item;
  });

  form.push({name: "booking_id", value: meetingData.booking_id});

  form.push({name: "timezone", value: "Asia/Jakarta"});

  Swal.fire({
      title: 'Confirmation?',
      text: "Are you sure to reschedule this meeting? ",
      type: "warning",
      showCancelButton: true,
      confirmButtonText: 'Reschedule Meeting !',
      cancelButtonText: 'Close !',
      reverseButtons: true
  }).then((result) => {
      if (result.value) {
          $.ajax({
              url: bsApiSmr + ajax.url.post_reschedule,
              type: "post",
              dataType: "json",
              data: $.param(form),
              beforeSend: function() {
                  showLoading();
              },
              success: function(data) {
                  Swal.close();
                  if (data.status == "success") {
                    $("#rescheduleModal").modal("hide");
                    $("#navbar-back").trigger("click");
                  } else {
                    var msg = "Failed to save new schedule, please try again !!!";
                    showNotification(msg, "error");
                  }
              },
              error: function(err) {
                var msg = "Failed to save new schedule, please try again !!!";
                showNotification(msg, "error");
              }
          })
      }
  });
});