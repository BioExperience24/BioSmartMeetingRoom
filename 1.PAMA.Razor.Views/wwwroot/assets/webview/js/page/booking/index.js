$(document).ready(function () {
  get_pantry_transactions();

  function get_pantry_transactions({
    startDate = new Date(),
    endDate = new Date(),
    draw = 1,
    start = 0,
    length = 10,
    searchValue = '',
    searchRegex = false,
    bookingOrganizer = "",
    bookingRoom = "",
    sortColumn = "no",
    sortDir = "desc",
  } = {}) {
    const bookingDate = formatDateRange(startDate, endDate);
    const params = new URLSearchParams({
      draw,
      start,
      length,
      "search[value]": searchValue,
      "search[regex]": searchRegex,
      booking_date: bookingDate,
      booking_organizer: bookingOrganizer,
      booking_room: bookingRoom,
      sort_column: sortColumn,
      sort_dir: sortDir,
      _: Date.now(), // to prevent caching
    });

    const url = `${bsApiSmr}${
      ajax.url.get_datatable_booking
    }?${params.toString()}`;
    $.ajax({
      url: url, // Use the correctly formatted URL
      type: "GET",
      dataType: "json",
      beforeSend: function () {
        // $("#id_loader").html('<div class="linePreloader"></div>');
      },
      success: function (data) {
        if (data.status === "success") {
          $("#meeting-list").empty(); // Clear the list before appending new data
          var col = data.collection.data;

          col.sort((a, b) => new Date(b.updated_at) - new Date(a.updated_at));

          var html = col
            .map((meeting) => {
              return `
                <li class="media" data-meeting-id="${meeting.id}">
                            <img class="mr-3 rounded" width="55" src="${bsApi}Room/GetRoomDetailView/${
                meeting.room_image
              }?h=80&noCache=false" alt="Room 5" />
                            <div class="media-body">
                                <div class="float-right">
                                    ${setStatus(meeting)}
                                </div>
                                <div class="media-title">${meeting.title}</div>
                                <div class="text-muted text-small">
                                    <strong>${meeting.booking_date}</strong>
                                    <strong>${meeting.time}</strong>
                                </div>
                                <div class="mt-1">
                                    <button class="btn btn-primary btn-sm view-details" data-meeting-id="${
                                      meeting.id
                                    }">
                                        View Details
                                    </button>
                                </div>
                            </div>
                </li>`;
            })
            .join(""); // Convert array to string

          $("#meeting-list").html(html); // Append the generated HTML
        } else {
          var msg = "Your session is expired, login again !!!";
          showNotification("alert-danger", msg, "top", "center");
        }
        // $("#id_loader").html("");
      },
      error: errorAjax,
    });
  }

  function formatDateRange(start, end) {
    const format = (date) => {
      const mm = String(date.getMonth() + 1).padStart(2, "0");
      const dd = String(date.getDate()).padStart(2, "0");
      const yyyy = date.getFullYear();
      return `${mm}/${dd}/${yyyy}`;
    };
    return `${format(start)} - ${format(end)}`;
  }

  function setStatus(item) {
    var status = "";

    let momentEnd = moment(item.server_end);
    let momentStart = moment(item.server_start);
    let extendTime = item.extended_duration - 0;
    let diffMomentEnd = momentEnd.add(extendTime, "minutes");

    if (item.is_approve == 1) {
      if (moment().unix() > momentEnd.unix()) {
        status = "<span class='badge bg-danger'>Expired</span>";
      } else if (
        moment().unix() <= momentEnd.unix() &&
        moment().unix() >= momentStart.unix()
      ) {
        status = "<span class='badge bg-success'>In progress</span>";
      } else if (momentStart.diff(moment(), "minutes") <= 0) {
        status = "<span class='badge bg-primary'>Ongoing</span>";
      } else if (moment().unix() <= moment(item.start).unix()) {
        status = "<span class='badge bg-primary'>Queue</span>";
      } else if (moment().diff(diffMomentEnd) >= 0) {
        status = "<span class='badge bg-success'>Expired</span>";
      }
    } else {
      if (moment().unix() > momentEnd.unix()) {
        status = "<span class='badge bg-danger'>Expired</span>";
      } else if (moment().diff(diffMomentEnd) >= 0) {
        status = "<span class='badge bg-success'>Expired</span>";
      } else {
        status = "<span class='badge bg-warning text-dark'>Pending</span>";
      }
    }

    if (item.is_alive - 0 == 0) {
      status = "<span class='badge bg-warning text-dark'>Pending</span>";
    }

    if (item.is_approve == 2) {
      status = "<span class='badge bg-danger'>Rejected</span>";
    }

    if (item.is_canceled == 1) {
      status = `<span class='badge bg-danger' style='cursor:pointer;' onclick="openPopupReasonCanceled($(this))">Canceled</span>`;
    }

    if (item.is_expired == 1 || item.end_early_meeting == 1) {
      var colorClass = item.end_early_meeting == 1 ? "bg-success" : "bg-danger";
      status = `<span class='badge ${colorClass}'>Expired</span>`;
    }

    return status;
  }

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
        Yesterday: [moment().subtract(1, "days"), moment().subtract(1, "days")],
        "Last 7 Days": [moment().subtract(6, "days"), moment()],
        "Last 30 Days": [moment().subtract(29, "days"), moment()],
        "This Month": [moment().startOf("month"), moment().endOf("month")],
        "Last Month": [
          moment().subtract(1, "month").startOf("month"),
          moment().subtract(1, "month").endOf("month"),
        ],
      },
      startDate: moment().startOf("day"),
      endDate: moment().endOf("day"),
    },
    function (start, end, label) {
      get_pantry_transactions({
        startDate: start.toDate(),
        endDate: end.toDate(),
      });
    }
  );
  // Inisialisasi Timepicker dengan interval 5 menit
  //   $("#newStartTime").timepicker({
  //     timeFormat: "H:i", // Format 24 jam (contoh: 09:00)
  //     step: 5, // Loncat 5 menit
  //     minTime: "00:00", // Batas bawah
  //     maxTime: "23:55", // Batas atas
  //     dynamic: false,
  //     dropdown: true,
  //     scrollbar: true,
  //   });

  //   $("#newEndTime").timepicker({
  //     timeFormat: "H:i",
  //     step: 5, // Loncat 5 menit
  //     minTime: "00:00",
  //     maxTime: "23:55",
  //     dynamic: false,
  //     dropdown: true,
  //     scrollbar: true,
  //   });

  // Meeting dummy data
  const meetingData = {
    1: {
      title: "Weekly Meeting / Gedung A / Room B",
      status: "Active",
      date: "05/03/2025",
      time: "9:00 - 10:00",
      host: "John Doe",
      description:
        "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugit, minima sunt quos ad pariatur nulla doloremque accusantium.",
      attendees: "2 Persons",
      internal: [
        { name: "Person 1", role: "Bio / IT", isHost: true },
        { name: "Person 2", role: "Bio / IT", isHost: false },
      ],
      external: [{ name: "External Person 1", role: "Guest / Vendor" }],
    },
    2: {
      title: "PIC Meeting / Gedung B / Room C",
      status: "Cancelled",
      date: "05/03/2025",
      time: "9:00 - 10:00",
      host: "Person 1",
      description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit.",
      attendees: "1 Person",
      internal: [{ name: "Person 1", role: "Manager", isHost: true }],
      external: [],
    },
  };

  // Filter list
  //   $("#filterForm").on("change", "#startDate, #statusFilter", function () {
  //     const startDate = $("#startDate").val();
  //     const statusFilter = $("#statusFilter").val();

  //     $("#meeting-list .media").each(function () {
  //       const dateText = $(this)
  //         .find(".text-small")
  //         .text()
  //         .split(" - ")[0]
  //         .trim();
  //       const status = $(this).find(".badge").text();
  //       const meetingDate = new Date(dateText.split("/").reverse().join("-"));
  //       const start = startDate ? new Date(startDate) : null;

  //       let show = true;
  //       if (start && meetingDate < start) show = false;
  //       if (statusFilter && status !== statusFilter) show = false;

  //       $(this).toggle(show);
  //     });
  //   });
  $("#statusFilter").change(function () {
    const statusFilter = $(this).val();
    $("#meeting-list .media").each(function () {
      const statusText = $(this).find(".badge").text().trim();
      const show = !statusFilter || statusText === statusFilter;

      $(this).toggle(show);
    });
  });

  // View details click
  $(".view-details").click(function () {
    const meetingId = $(this).data("meeting-id");
    const data = meetingData[meetingId];

    $("#meeting-title").text(data.title);
    $("#meeting-status")
      .text(data.status)
      .removeClass()
      .addClass(
        `badge p-2 badge-${
          data.status === "Active"
            ? "success"
            : data.status === "Cancelled"
            ? "danger"
            : "warning"
        }`
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
                    <img src="../assets/img/avatar/avatar-3.png" class="rounded-circle mr-3" width="40" height="40" alt="${
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
                    <img src="../assets/img/example-image.jpg" class="rounded-circle mr-3" width="40" height="40" alt="${att.name}" />
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

    $("#navbar-icon i").removeClass("fa-home").addClass("fa-arrow-left");
  });

  // Kembali ke list
  function returnToList() {
    $("#meeting-details-section").hide();
    $("#meeting-list-section").show();
    $("#navbar-icon i").removeClass("fa-arrow-left").addClass("fa-home");
  }

  // End meeting dengan SweetAlert
  $("#end-meeting").click(function () {
    swal({
      title: "Are you sure?",
      text: "This meeting will be ended!",
      icon: "warning",
      buttons: true,
    }).then((willEnd) => {
      if (willEnd) {
        swal("Meeting has ended!", { icon: "success" });
        returnToList();
      }
    });
  });

  // Cancel meeting dengan SweetAlert
  $("#cancel-meeting").click(function () {
    swal({
      title: "Are you sure?",
      text: "This meeting will be cancelled!",
      icon: "warning",
      buttons: true,
      dangerMode: true,
    }).then((willCancel) => {
      if (willCancel) {
        swal("Meeting has been cancelled!", { icon: "success" });
        returnToList();
      }
    });
  });

  // Tombol back
  $("#navbar-icon").click(function (e) {
    e.preventDefault();
    if ($("#meeting-details-section").is(":visible")) {
      returnToList();
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
  $("#saveReschedule").click(function () {
    const newDate = $("#newDate").val();
    const newStartTime = $("#newStartTime").val();
    const newEndTime = $("#newEndTime").val();

    if (newDate && newStartTime && newEndTime) {
      const dateParts = newDate.split("-");
      const formattedDate = `${dateParts[2]}/${dateParts[1]}/${dateParts[0]}`;
      const newTime = `${newStartTime} - ${newEndTime}`;
      $("#meeting-date-time").html(
        `<strong>${formattedDate}</strong> - <strong>${newTime}</strong>`
      );
      $("#rescheduleModal").modal("hide");
      $("#rescheduleForm")[0].reset();
    } else {
      alert("Please fill in all fields.");
    }
  });

  function errorAjax(xhr, ajaxOptions, thrownError) {
    $("#id_loader").html("");
    if (ajaxOptions == "parsererror") {
      var msg = "Status Code 500, Error Server bad parsing";
      swalShowNotification("alert-danger", msg, "bottom", "left");
    } else {
      var msg =
        "Status Code " + xhr.status + " Please check your connection !!!";
      swalShowNotification("alert-danger", msg, "bottom", "left");
    }
  }
});

function swalShowNotification(icon, title, loc = "", loc2 = "") {
  var ic = "";
  if (icon == "alert-success") {
    ic = "success";
  } else if (icon == "alert-danger") {
    ic = "danger";
  } else if (icon == "alert-warning") {
    ic = "warning";
  } else if (icon == "alert-info") {
    ic = "info";
  }
  Swal.fire(title, "", ic);
}