$.extend($.fn.dataTable.defaults, {
    language: {
        url: "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
    }
});

const renderText = (text) => {
    if (isNullOrWhiteSpace(text)) {
        return '';
    }

    return text.length > 100 ? text.substring(0, 100) + '...' : text;
}

const renderImage = (url) => {
    if (isNullOrWhiteSpace(url)) {
        return `<img src="/theme/assets/media/svg/files/blank-image.svg" class="w-50px align-middle">`;
    }

    return `<img src="${url}" class="w-50px align-middle">`;
}

const renderDateTime = (dateTime) => {
    return moment(dateTime, 'YYYY/MM/DD').locale('tr').format("LL");
}

const renderEditButton = (id) => {
    let str = '<a href="Edit/' + id + '" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1"><span class="svg-icon svg-icon-3"><svg xmlns="http://www.w3.org/2000/svg" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"><path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)"></path><path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3"></path></svg></span></a>';
    return str;
}

const renderActiveOrPassiveBadge = (data) => {
    const badge = data ? 'success' : 'danger';
    const badgeName = data ? 'aktif' : 'pasif';
    return `<span class="badge badge-light-${badge} fs-8 fw-bolder">${badgeName}</span>`;
}

const renderStatusIconBadge = (data) => {
    if (data) {
        return '<span class="svg-icon svg-icon-primary svg-icon-2x"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><polygon points="0 0 24 0 24 24 0 24"/><path d="M6.26193932,17.6476484 C5.90425297,18.0684559 5.27315905,18.1196257 4.85235158,17.7619393 C4.43154411,17.404253 4.38037434,16.773159 4.73806068,16.3523516 L13.2380607,6.35235158 C13.6013618,5.92493855 14.2451015,5.87991302 14.6643638,6.25259068 L19.1643638,10.2525907 C19.5771466,10.6195087 19.6143273,11.2515811 19.2474093,11.6643638 C18.8804913,12.0771466 18.2484189,12.1143273 17.8356362,11.7474093 L14.0997854,8.42665306 L6.26193932,17.6476484 Z" fill="#000000" fill-rule="nonzero" transform="translate(11.999995, 12.000002) rotate(-180.000000) translate(-11.999995, -12.000002) "/></g></svg></span>';
    } else {
        return '<span class="svg-icon svg-icon-primary svg-icon-2x"><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><g transform="translate(12.000000, 12.000000) rotate(-45.000000) translate(-12.000000, -12.000000) translate(4.000000, 4.000000)" fill="#000000"><rect x="0" y="7" width="16" height="2" rx="1"/><rect opacity="0.3" transform="translate(8.000000, 8.000000) rotate(-270.000000) translate(-8.000000, -8.000000) " x="0" y="7" width="16" height="2" rx="1"/></g></g></svg></span>';
    }
}

const renderSortBadge = (identifier, actionName) => {
    let str = `<a href="${actionName}/${identifier}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1"><span class="svg-icon svg-icon-muted svg-icon-2hx"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"><path d="M18.4 18H16C15.7 18 15.5 17.9 15.3 17.7L12.5 14.9C12.1 14.5 12.1 13.9 12.5 13.5C12.9 13.1 13.5 13.1 13.9 13.5L16.4 16H18.4V18ZM16 6C15.7 6 15.5 6.09999 15.3 6.29999L11 10.6L6.70001 6.29999C6.50001 6.09999 6.3 6 6 6H3C2.4 6 2 6.4 2 7C2 7.6 2.4 8 3 8H5.60001L9.60001 12L5.60001 16H3C2.4 16 2 16.4 2 17C2 17.6 2.4 18 3 18H6C6.3 18 6.50001 17.9 6.70001 17.7L16.4 8H18.4V6H16Z" fill="black"/><path opacity="0.3" d="M21.7 6.29999C22.1 6.69999 22.1 7.30001 21.7 7.70001L18.4 11V3L21.7 6.29999ZM18.4 13V21L21.7 17.7C22.1 17.3 22.1 16.7 21.7 16.3L18.4 13Z" fill="black"/></svg></span></a>`;
    return str;
}

const renderLandline = (value) => {
    let i = 0;
    let pattern = '####-###-##-##';
    const phone = value.toString();
    return pattern.replace(/#/g, _ => phone[i++]);
}