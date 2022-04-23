$("#success-alert").fadeTo(2000, 500).slideUp(500, function () {
    $("#success-alert").slideUp(500);
});

$("#alert-warning").fadeTo(30000, 500).slideUp(500, function () {
    $("#alert-warning").slideUp(500);
});

function mascaraMutuario(o, f) {
    v_obj = o
    v_fun = f
    setTimeout('execmascara()', 1)
}

function execmascara() {
    v_obj.value = v_fun(v_obj.value)
}

function cpfCnpj(v) {

    v = v.replace(/\D/g, "")

    if (v.length <= 14) {

        v = v.replace(/(\d{3})(\d)/, "$1.$2")

        v = v.replace(/(\d{3})(\d)/, "$1.$2")

        v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2")

    } else {

        v = v.replace(/^(\d{2})(\d)/, "$1.$2")

        v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3")

        v = v.replace(/\.(\d{3})(\d)/, ".$1/$2")

        v = v.replace(/(\d{4})(\d)/, "$1-$2")

    }

    let max = v.length > 18 ? 18 : v.length;
    v = v.substring(0, max);

    return v

}
