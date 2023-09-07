document.addEventListener("DOMContentLoaded", () => {
    console.log("Entered userBasket.js File");

    var deleteButtons = document.getElementsByClassName("deleteProductFromBasket");

    Array.from(deleteButtons).forEach(button => {
        console.log("ENTERED THIS BUTTON NOW");


        button.addEventListener('click', (event) => {
            event.preventDefault();

            const productId = +button.getAttribute('data-product-id');
            const currentUserId = +button.getAttribute('data-user-id');

            let url = event.target.getAttribute('href');

            console.log("URL: ", url);
            console.log("DATA FROM USERBASKET ");
            console.log(productId);
            console.log(currentUserId);

            fetch(url, {
                method: 'POST',
                body: JSON.stringify({
                    productId: productId,
                    userId: currentUserId
                })
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error("Network response is not ok");
                    }
                    return response.json();
                })
                .then(data => {
                    console.log(data);
                })
                .then(() => {
                    window.location.reload();
                })
                .catch(error => {
                    console.log(error);
                    console.log("There was a problem with your fetch mirror");
                })

        })

    })

});