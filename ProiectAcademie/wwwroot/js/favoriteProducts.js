document.addEventListener("DOMContentLoaded", () => {

    var deleteButtons = document.getElementsByClassName("deleteFavoriteProductButton");

    Array.from(deleteButtons).forEach(button => {

        console.log("ENTERED HERE");

        button.addEventListener('click', (event) => {

            event.preventDefault();

            const productId = +button.getAttribute('data-product-id');
            const currentUserId = +button.getAttribute('data-user-id');

            let url = event.target.getAttribute('href');



            console.log("THE URL IS " + url);

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
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
                    console.log("There was a problem with your fetch method");
                })
        });
    });

    console.log(deleteButtons);
});