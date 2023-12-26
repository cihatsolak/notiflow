const inputConvertToDateTimePicker = (id, minDate, maxDate) => {
   $(`#${id}`).daterangepicker({
      timePicker24Hour: true,
      applyClass: "btn btn-xs btn-success",
      cancelClass: "btn btn-xs btn-warning",
      singleDatePicker: true,
      timePicker: true,
      minDate: minDate,
      maxDate: maxDate,
      "locale": {
         "firstDay": 1,
         "format": 'DD/MM/YYYY HH:mm',
         "separator": " - ",
         "applyLabel": "Seç",
         "cancelLabel": "İptal",
         "fromLabel": "Dan",
         "toLabel": "a",
         "customRangeLabel": "Seç",
         "daysOfWeek": [
            "Pz",
            "Pt",
            "Sl",
            "Çr",
            "Pr",
            "Cm",
            "Ct"
         ],
         "monthNames": [
            "Ocak",
            "Şubat",
            "Mart",
            "Nisan",
            "Mayıs",
            "Haziran",
            "Temmuz",
            "Ağustos",
            "Eylül",
            "Ekim",
            "Kasım",
            "Aralık"
         ]
      }
   });
}

const inputConvertToDateRangePicker = (id, minDate, maxDate) => {
   $(`#${id}`).daterangepicker({
      applyClass: "btn btn-xs btn-success",
      cancelClass: "btn btn-xs btn-warning",
      minDate: minDate,
      maxDate: maxDate,
      singleDatePicker: true,
      "locale": {
         "firstDay": 1,
         "format": 'DD.MM.YYYY',
         "separator": " - ",
         "applyLabel": "Seç",
         "cancelLabel": "İptal",
         "fromLabel": "Dan",
         "toLabel": "a",
         "customRangeLabel": "Seç",
         "daysOfWeek": [
            "Pz",
            "Pt",
            "Sl",
            "Çr",
            "Pr",
            "Cm",
            "Ct"
         ],
         "monthNames": [
            "Ocak",
            "Şubat",
            "Mart",
            "Nisan",
            "Mayıs",
            "Haziran",
            "Temmuz",
            "Ağustos",
            "Eylül",
            "Ekim",
            "Kasım",
            "Aralık"
         ]
      }
   });
}