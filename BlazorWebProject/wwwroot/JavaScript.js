//쿠키를 쓰는 스크립트
window.WriteCookie = {
    WriteCookie: function (name, value, days) {
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    }
}
//쿠키를 읽는 스크립트
window.ReadCookie = {
    ReadCookie: function (cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
}
//쿠키를 삭제하는 스크립트
window.deleteCookie = function (cookieName) {
    document.cookie = cookieName + '=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
}


//모달창을 띄우는 스크립트
window.Blazor = window.Blazor || {};
window.Blazor.showModal = (id) => {
    var modal = document.getElementById(id);
    if (modal) {
        modal.classList.add('show');
        modal.style.display = 'block';
    }
};