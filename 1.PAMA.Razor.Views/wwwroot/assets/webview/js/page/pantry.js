function toggleFilter() {
  $("#filterForm").collapse("toggle");
}

$("#btn-cancel-order").hide();

// data order
const orderData = {};

var pantryPackagesCollection;
var pantryPackageDetailCollection;
// update pantry order

function get_pantry_transactions(startDate = "", endDate = "", status = "") {
  var url =
    bsApiSmr +
    ajax.url.get_pantry_transactions +
    `?start=${startDate}&end=${endDate}&pantryId=&orderSt=${status}`;

  $.ajax({
    url: url, // Use the correctly formatted URL
    type: "GET",
    dataType: "json",
    beforeSend: function () {
      // $("#id_loader").html('<div class="linePreloader"></div>');
    },
    success: function (data) {
      if (data.status === "success") {
        var col = data.collection;

        col.sort((a, b) => new Date(b.updated_at) - new Date(a.updated_at));

        var html = col
          .map((meeting) => {
            let bookDate = `${moment(meeting.date_booking).format(
              "DD MMM YYYY"
            )} ${moment(meeting.booking_start).format("HH:mm")} - ${moment(
              meeting.booking_end
            ).format("HH:mm")}`;

            return `
                <div class="card mb-3 order-item" data-order-id="${
                  meeting.id
                }" style="cursor: pointer">
                    <div class="card-body">
                        <div class="media align-items-center">
                            <div class="text-center border-right" style="min-width: 120px">
                                <h5 class="media-title mb-1">${meeting.id}</h5>
                                <span class="text-muted d-block">${
                                  meeting.room_name
                                }</span>
                                ${statusBadge(meeting.order_status)}
                            </div>
                            <div class="media-body pl-3">
                                <h5 class="media-title mb-1">${
                                  meeting.title
                                }</h5>
                                <div class="text-muted text-small mb-2">${bookDate}</div>
                            </div>
                        </div>
                    </div>
                </div>`;
          })
          .join(""); // Convert array to string

        $("#order-list").html(html); // Append the generated HTML
      } else {
        var msg = "Your session is expired, login again !!!";
        swalShowNotification("alert-danger", msg, "top", "center");
      }
      // $("#id_loader").html("");
    },
    error: errorAjax,
  });
}

async function get_meeting_by_id(id = "") {
  var url = bsApiSmr + ajax.url.get_booking_by_id + `/${id}`;

  try {
    const data = await new Promise((resolve, reject) => {
      $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: function (response) {
          if (response.status === "success") {
            resolve(response.collection);
          } else {
            swalShowNotification(
              "alert-danger",
              "Your session is expired, login again !!!",
              "top",
              "center"
            );
            reject("Session expired");
          }
        },
        error: function (error) {
          reject(error);
        },
      });
    });

    return data; // Return the fetched data
  } catch (error) {
    return null; // Handle errors properly
  }
}
async function setRoomSelect() {
  var url = bsApiSmr + ajax.url.get_meeting_in_progress;
  await $.ajax({
    url: url,
    type: "GET",
    dataType: "json",
    success: function (data) {
      if (data.status === "success") {
        var meetingListCollection = data.collection;

        $.each(meetingListCollection, function (_, item) {
          $("#roomSelect").empty();
          $("#roomSelect").append(`<option value="">-- Select Room --</option>`);

          let opt = document.createElement("option");
          let date = `${moment(item.date).format("DD MMM YYYY")} ${moment(
            item.start
          ).format("HH:mm")} - ${moment(item.end).format("HH:mm")}`;

          $(opt).html(
            `${item.title} - ${item.room_name} <br> <small><b>(${date})</b></small>`
          );
          $(opt).val(item.booking_id);
          $(opt).data("attendees", item.attendees);

          $("#roomSelect").append(opt);
        });
      } else {
        swalShowNotification(
          "alert-danger",
          "Your session is expired, login again !!!",
          "top",
          "center"
        );
      }
    },
    error: errorAjax,
  });
}

async function get_pantry_packages() {
  var url = bsApiSmr + ajax.url.get_pantry_packages;
  try {
    var data = await $.ajax({
      type: "Get",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      url: url,
    });

    if (data.status == "success") {
      pantryPackagesCollection = data.collection;

      initPackageList();
    } else {
      var msg = "Your session is expired, login again !!!";
      showNotification("alert-danger", msg, "top", "center");
    }
  } catch (err) {
    // console.log(err);
    errorAjax;
  }
}

function initPackageList() {
  $("#categorySelect").empty();
  $("#categorySelect").append("<option value=''>-- Select Package --</option>");

  $.each(pantryPackagesCollection, function (_, item) {
    let opt = document.createElement("option");

    $(opt).text(`${item.name}`);
    $(opt).val(item.id);

    $("#categorySelect").append(opt);
  });
  select_enable();
}

$("#categorySelect").on("change", function () {
  const t = $(this);
  if (t.val() !== "") {
    getPantryPackageDetail(t.val());
  }
});

async function getPantryPackageDetail(pantryPackageId) {
  var url = bsApiSmr + ajax.url.get_pantry_package_by_id + pantryPackageId;
  try {
    var data = await $.ajax({
      type: "Get",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      url: url,
    });

    if (data.status == "success") {
      let collection = data.collection;
      let detail = [];

      $.each(collection.detail, function (_, item) {
        detail.push({
          id: item.id,
          pantry_id: item.pantry_id,
          name: item.name,
          note: "",
          description: item.description,
          qty: ttlAttendees ?? 0,
        });
      });

      pantryPackageDetailCollection = detail;

      genPantryDetail();
    } else {
      var msg = "Your session is expired, login again !!!";
      showNotification("alert-danger", msg, "top", "center");
    }
  } catch (err) {
    // console.log(err);
    errorAjax;
  }
}

function genPantryDetail() {
  $("#foodItemsDiv").empty();
  if (pantryPackageDetailCollection != null) {
    let itemHtml;

    $("#foodItemsDiv").append("<label>Food Items</label>");
    $.each(pantryPackageDetailCollection, function (key, item) {
      itemHtml = `
                <div class="col-12" id="pkg-detail-row-${item.id}">
                  <label>${item.name}</label>
                  <div class="row">
                    <div class="col-4">
                      <input autocomplete="off" type="number" min="0" class="form-control number-without-arrow inp_qty_package_detail" placeholder="Qty" value="${item.qty}" onkeyup="checkQty(this)">
                    </div>
                    <div class="col-8">
                      <input autocomplete="off" type="text" class="form-control number-without-arrow inp_note_package_detail" placeholder="Note" value="${item.note}">
                    </div>
                  </div>
                </div>
      `;

      $("#foodItemsDiv").append(itemHtml);
      $(`#pkg-detail-row-${item.id}`).data("pkgDetailId", item.id);
    });
  }
}

$("#categorySelect").on("change", function () {
  const t = $(this);
  const ttl = t.find(":selected").data("attendees");
  ttlAttendees = ttl;

  $.each(pantryPackageDetailCollection, function (key, item) {
    pantryPackageDetailCollection[key]["qty"] = parseInt(ttlAttendees);
  });

  genPantryDetail();
});

function createNewOrder() {
  $("#frm_new_order").trigger("submit");
}

$(document).on("keyup", ".inp_qty_package_detail", function () {
  const t = $(this);
  let row = t.closest(".col-12");
  let qty = t.val();
  let packageDetailID = row.data("pkgDetailId");

  $.each(pantryPackageDetailCollection, function (key, item) {
    if (item.id == packageDetailID) {
      pantryPackageDetailCollection[key]["qty"] = parseInt(qty);
    }
  });
});

$(document).on("keyup", ".inp_note_package_detail", function () {
  const t = $(this);
  let row = t.closest(".col-12");
  let note = t.val();
  let packageDetailID = row.data("pkgDetailId");

  $.each(pantryPackageDetailCollection, function (key, item) {
    if (item.id == packageDetailID) {
      pantryPackageDetailCollection[key]["note"] = note;
    }
  });
});

var iajx = "";
$("#frm_new_order").on("submit", function (e) {
  e.preventDefault();
  if (iajx != "") {
    iajx.abort();
  }

  const form = $(this);
  let data = form.serializeArray();

  var meeting_id = $("#roomSelect").val();
  var pantry_package_id = $("#categorySelect").val();

  if (
    !meeting_id ||
    !pantry_package_id ||
    meeting_id.trim() === "" ||
    pantry_package_id.trim() === ""
  ) {
    Swal.fire({
      title: "Validation Error",
      text: "Please select both Room and Package before submitting.",
      icon: "warning",
      confirmButtonText: "OK",
    });
    return; // Stop form submission
  }

  data.push({ name: "booking_id", value: meeting_id });
  data.push({ name: "meeting_category", value: pantry_package_id });

  if (pantryPackageDetailCollection != null) {
    $.each(pantryPackageDetailCollection, function (_, item) {
      let d = {
        menu_id: item.id,
        pantry_id: item.pantry_id,
        qty: item.qty,
        note: item.note,
      };
      data.push({ name: "menu_items[]", value: JSON.stringify(d) });
    });
  }

  var reportValidity = form[0].reportValidity();

  if (reportValidity) {
    var url = bsApiSmr + ajax.url.create_order_package;
    iajx = $.ajax({
      type: "POST",
      // contentType: "application/json; charset=utf-8",
      dataType: "json",
      url: url,
      // data: objectify_form(form.serializeArray()),
      // data: form.serialize(),
      data: $.param(data),
      beforeSend: function () {
        $("#id_loader").html('<div class="linePreloader"></div>');
        disableAllForm();
      },
      success: function (data) {
        $("#id_loader").html("");
        enabledAllForm();
        if (data.status == "success") {
          $("#createOrderModal").modal("hide");
          Swal.fire({
            title: "Success",
            text: data.msg,
            icon: "success",
            confirmButtonText: "OK",
          });
        } else {
          var msg = "Your session is expired, login again !!!";
          if (data.msg != undefined || data.msg != "") {
            msg = data.msg;
          }
          Swal.fire({
            title: "Failed",
            text: msg,
            icon: "danger",
            confirmButtonText: "OK",
          });
        }
      },
      error: errorAjax,
      complete: function (data) {
        enabledAllForm();
        get_pantry_transactions();
      },
    });
  }
});

function disableAllForm() {
  $(":input", "#createOrderModal").prop("disabled", true);
}

function enabledAllForm() {
  $(":input", "#createOrderModal").prop("disabled", false);
}

//smr.beit.co.id/api/

function statusBadge(status) {
  var gPantry = [
    { id: 0, value: "Order not yet processed", class: "badge-secondary" },
    { id: 1, value: "Order processed", class: "badge-warning" },
    { id: 2, value: "Order Delivered", class: "badge-info" },
    { id: 3, value: "Order Completed", class: "badge-success" },
    { id: 4, value: "Order canceled", class: "badge-danger" },
    { id: 5, value: "Order rejected", class: "badge-dark" },
  ];

  var statusObj = gPantry.find((item) => item.value === status);

  if (statusObj) {
    return `<span class="badge ${statusObj.class} mt-2">${statusObj.value}</span>`;
  } else {
    return `<span class="badge badge-light mt-2">Unknown Status</span>`;
  }
}

function errorAjax(xhr, ajaxOptions, thrownError) {
  $("#id_loader").html("");
  if (ajaxOptions == "parsererror") {
    var msg = "Status Code 500, Error Server bad parsing";
    swalShowNotification("alert-danger", msg, "bottom", "left");
  } else {
    var msg = "Status Code " + xhr.status + " Please check your connection !!!";
    swalShowNotification("alert-danger", msg, "bottom", "left");
  }
}

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

get_pantry_transactions();

function checkQty(t)
{
  if ($(t).val() < 0) {
    $(t).val(0);
    $(t).focus();
  }
}

$(document).ready(function () {
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
      get_pantry_transactions(
        start.format("YYYY-MM-DD"),
        end.format("YYYY-MM-DD")
      );
      //   console.log(
      //     "New range selected: " +
      //       start.format("YYYY-MM-DD") +
      //       " - " +
      //       end.format("YYYY-MM-DD") +
      //       " (Label: " +
      //       label +
      //       ")"
      //   );
    }
  );

  const originalContent = $("#content-container").html();

  // inisialisasi select2
  function initializeSelect2() {
    $("#roomSelect").select2({
      theme: "bootstrap4",
      placeholder: "-- Select Room --",
      allowClear: true,
      width: "100%",
      minimumResultsForSearch: 1,
      dropdownParent: $("#createOrderModal"),
    });
    $("#categorySelect").select2({
      theme: "bootstrap4",
      placeholder: "-- Select Category --",
      allowClear: true,
      width: "100%",
      minimumResultsForSearch: 1,
      dropdownParent: $("#createOrderModal"),
    });
  }

  // Show or hide the Food Items div based on the selected values
  function toggleFoodItemsDiv() {
    const roomSelected = $("#roomSelect").val();
    const categorySelected = $("#categorySelect").val();
    if (roomSelected && categorySelected) {
      $("#foodItemsDiv").show();
      // if (categorySelected === "Small Meeting") {
      //   $("#kopiDiv, #rotiDiv, #waterDiv").show();
      //   $("#matchaDiv, #rendangDiv, #jaheDiv").hide();
      // } else if (categorySelected === "Medium Meeting") {
      //   $("#kopiDiv, #rotiDiv, #waterDiv, #matchaDiv").show();
      //   $("#rendangDiv, #jaheDiv").hide();
      // } else if (categorySelected === "Large Meeting") {
      //   $(
      //     "#kopiDiv, #rotiDiv, #waterDiv, #matchaDiv, #rendangDiv, #jaheDiv"
      //   ).show();
      // }
    } else {
      $("#foodItemsDiv").hide();
    }
  }

  // Attach change event listeners to the select elements
  $("#roomSelect, #categorySelect").on("change", toggleFoodItemsDiv);

  // logic filter
  $("#filterForm").on("change", "#startDate, #statusFilter", function () {
    const startDate = $("#startDate").val();
    const statusFilter = $("#statusFilter").val();

    $("#order-list .order-item").each(function () {
      const dateText = $(this)
        .find(".text-small")
        .text()
        .split(" - ")[0]
        .trim();
      const status = $(this).find(".badge").text();
      const orderDate = new Date(dateText.split("/").reverse().join("-"));
      const start = startDate ? new Date(startDate) : null;

      let show = true;
      if (start && orderDate < start) show = false;
      if (statusFilter && status !== statusFilter) show = false;

      $(this).toggle(show);
    });
  });

  function backToHome() {
    if ($("#detail-order-section").is(":visible")) {
      $("#content-container").html(originalContent);
      $("#navbar-icon i").removeClass("fa-arrow-left").addClass("fa-home");
      attachListEvents();
      initializeSelect2();
      get_pantry_transactions();
    }
  }
  // navbar back
  $("#navbar-icon").click(function (e) {
    e.preventDefault();
    backToHome();
  });

  function attachListEvents() {
    $("#filterForm").on("change", "#startDate, #statusFilter", function () {
      const startDate = $("#startDate").val();
      const statusFilter = $("#statusFilter").val();

      $("#order-list .order-item").each(function () {
        const dateText = $(this)
          .find(".text-small")
          .text()
          .split(" - ")[0]
          .trim();
        const status = $(this).find(".badge").text();
        const orderDate = new Date(dateText.split("/").reverse().join("-"));
        const start = startDate ? new Date(startDate) : null;

        let show = true;
        if (start && orderDate < start) show = false;
        if (statusFilter && status !== statusFilter) show = false;

        $(this).toggle(show);
      });
    });

    $("#order-list").on("click", ".order-item", async function (e) {
      e.preventDefault();
      const orderId = $(this).data("order-id");
      const data = await get_meeting_by_id(orderId);

      $("#content-container").load(
        "/Webview/PantryOrder/Index?handler=DetailOrderContent",
        function () {
          $("#order-id").text(data.pantry_transaksi_id);
          $("#order-title").text(data.booking_title);
          $("#order-room").text(data.room_name);
          $("#order-category").text(data.pantry_package_name);
          var room_image = `${bsApi}Room/GetRoomDetailView/${data.room_image}?h=80&noCache=false`;
          $("#room-image").attr("src", room_image);

          let bookDate = moment(data.booking_date).format("DD/MM/YYYY"); // Format date only
          let startTime = moment(data.booking_start).format("HH:mm"); // Format start time
          let endTime = moment(data.booking_end).format("HH:mm"); // Format end time

          $("#order-date-time").html(
            `<strong>${bookDate}</strong> - <strong>${startTime} - ${endTime}</strong>`
          );

          $("#order-items").html(`
                <ul class="list-group list-group-flush">
                  ${data.order_detail
                    .map(
                      (item) => `
                    <li class="list-group-item d-flex justify-content-between align-items-center">
                    <div>
                      <span><i class="text-brown mr-2"></i>${item.name}</span>
                      <small class="text-muted d-block ml-2">${item.note}</small>
                    </div>
                      <span><i class="text-brown mr-2"></i>Qty: ${item.qty}</span>
                    </li>
                  `
                    )
                    .join("")}
                </ul>
              `);

          $("#order-status").html(statusBadge(data.order_status_name));

          let lastUpdated = moment(data.update_at).format("DD MMM YYYY, HH:mm");
          $("#order-last-updated").text(`Last updated: ${lastUpdated}`);

          $("#navbar-icon i").removeClass("fa-home").addClass("fa-arrow-left");
          $("#detail-order-section").show();

          var btnCancel = `
          <div class="col-12">
            <div class="fixed-bottom bg-white p-3 shadow">
                <div class="d-flex justify-content-between">
                    <button
                        class="btn btn-danger flex-fill mr-2 py-3"
                        id="cancel-order"
                        style="font-size: 18px"
                    >
                        Cancel Order
                    </button>
                </div>
            </div>
            </div>
        `;

          if (data.order_status == 0) {
            var dateexpired = moment(data.expired_at).unix();
            var nowtime = moment().unix();
            if (nowtime > dateexpired) {
              status_order = "Order expired";
              expired = moment(data.expired_at).format("DD MMM YYYY hh:mm A");
              btnCancel = "";
            }
          } else {
            expired = moment(data.expired_at).format("DD MMM YYYY hh:mm A");
            btnCancel = "";
          }

          $("#btn-cancel-order").html(btnCancel);

          if (data.order_status == 0) {
            var dateexpired = moment(data.expired_at).unix();
            var nowtime = moment().unix();
            if (nowtime > dateexpired) {
              status_order = "Order expired";
              expired = moment(item.expired_at).format("DD MMM YYYY hh:mm A");
              btnCancel = "";
            } else {
              $("#btn-cancel-order").show();
              $("#cancel-order").click(function () {
                swal({
                  title: "Are you sure want cancel order?",
                  text: "You will cancel the data order " + orderId + " !",
                  type: "warning",
                  showCancelButton: true,
                  confirmButtonColor: "#3085d6",
                  cancelButtonColor: "#d33",
                  confirmButtonText: "Cancel Order !",
                  cancelButtonText: "Close !",
                  reverseButtons: true,
                }).then((result) => {
                  if (result.value) {
                    Swal.fire({
                      title: "Reason for Cancelation",
                      input: "text",
                      inputAttributes: {
                        autocapitalize: "off",
                      },
                      showCancelButton: true,
                      confirmButtonColor: "#3085d6",
                      cancelButtonColor: "#d33",
                      confirmButtonText: "Submit",
                      cancelButtonText: "Close !",
                      reverseButtons: true,
                      preConfirm: (result) => {
                        if (result == "" || result == null) {
                          return Swal.showValidationMessage(
                            `Reason for Cancelation is required`
                          );
                        }
                      },
                    }).then((result) => {
                      if (result.value !== undefined) {
                        var form = new FormData();
                        form.append("id", orderId);
                        form.append("note", result.value);
                        $.ajax({
                          url: bsApiSmr + ajax.url.post_cancel_order,
                          type: "POST",
                          data: form,
                          processData: false,
                          contentType: false,
                          dataType: "json",
                          beforeSend: function () {
                            $("#id_loader").html(
                              '<div class="linePreloader"></div>'
                            );
                            Swal.fire({
                              title: "Please Wait !",
                              html: "Process to cancel order",
                              allowOutsideClick: false,
                              onBeforeOpen: () => {
                                Swal.showLoading();
                              },
                            });
                          },
                          success: function (data) {
                            Swal.close();
                            $("#id_loader").html("");
                            if (data.status == "success") {
                              Swal.fire({
                                title: "Success",
                                html: "Succes cancel order",
                                icon: "success",
                              });
                              backToHome();
                            } else {
                              Swal.fire({
                                title: "Failed",
                                html: "Reject order is failed!!!",
                                icon: "danger",
                              });
                            }
                          },
                          complete: function () {
                            $("#id_loader").html("");
                            attachListEvents();
                            initializeSelect2();
                          },
                          error: errorAjax,
                        });
                      }
                    });
                  }
                });
              });
            }
          }
        }
      );
    });
  }

  attachListEvents();

  // Reset form data when the modal is closed
  $("#createOrderModal").on("hidden.bs.modal", function () {
    $("#frm_new_order")[0].reset();
    $("#roomSelect").val("").trigger("change");
    $("#categorySelect").val("").trigger("change");
    $("#foodItemsDiv").hide();
  });

  // event untuk create order button
  $(document).on("click", "#createOrderBtn", async function () {
    setRoomSelect();
    get_pantry_packages();
    $("#createOrderModal").modal("show");
    $("#createOrderModal").on("shown.bs.modal", function () {
      initializeSelect2();
      $("#roomSelect").select2("open");
      $("#roomSelect").select2("close");
      $("#categorySelect").select2("open");
      $("#categorySelect").select2("close");
    });
  });
});
