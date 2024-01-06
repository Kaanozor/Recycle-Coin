$(function () {
    $('#addBtn').on('click', function () {
        var carbon = $('#productSelect').find(":selected").data('carbon');
        var quentity = $('#Quantity').val();
        var result = carbon * quentity;
        var $total = $('#totalCarbon');
        $total.text(Number($total.text()) + result);
        $('#totalRecycle').text(Number($total.text())/25)
    });

    $('#sendBtn').on('click', function () {
        var key = $('#publicKey').val();
        var rcyc = Number($('#totalRecycle').text());

        fetch('http://localhost:3000/transaction/' + key + '/' + rcyc, {
            mode: 'no-cors',
            method: "get",
            headers: {
                "Content-Type": "application/json"
            }
        }).then((r) => alert('Gönderildi')).catch(e => alert('çalışmadı'))
    });
})