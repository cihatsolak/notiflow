const deleteAlert = (callBackFunction, param) => {
    Swal.fire({
        title: "Emin misin?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet, sil!",
        cancelButtonText: "Hayır, iptal et!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            callBackFunction(param);
        } else {
            Swal.fire(
                "İptal edildi",
                "Dosya güvende.",
                "warning"
            )
        }
    });

};

const showSweetAlert = (result, title) => {
    if (isNullOrWhiteSpace(title)) {
        title = result.succeeded ? "Başarılı!" : "Başarısız";
    }

    Swal.fire(
        title,
        result.statusMessage,
        result.succeeded ? "success" : "error"
    )
}
