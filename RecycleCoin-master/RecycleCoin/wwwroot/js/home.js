$(function () {
    
    $('#sendBtn').on('click', function () {
        var key = $('#publicKey').val();

        fetch('http://localhost:3000/transaction/mine/' + key, {
            mode: 'no-cors',
            method: "get",
            headers: {
                "Content-Type": "application/json"
            }
        }).then((r) => alert('Mining başladı')).catch(e => alert('Mine Başlatılamadı'))
    });
})