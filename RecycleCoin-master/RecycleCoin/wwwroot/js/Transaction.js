$(function () {

    $('#sendBtn').on('click', function () {
    var key = $('#publicKey').val();
    var key1 = $('#recipientAddress').val();
    var amount = $('#amount').val();


    fetch('http://localhost:3000/transaction/user/'+key+'/'+key1+'/' + amount, {
            mode: 'no-cors',
            method: "get",
            headers: {
                "Content-Type": "application/json"
            }
        }).then((r) => alert('Transaction Başarılı')).catch(e => alert('Transaction Başarısız'))
    });
})