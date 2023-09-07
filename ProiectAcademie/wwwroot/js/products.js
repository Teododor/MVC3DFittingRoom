document.addEventListener("DOMContentLoaded", () => {

    const hearts = document.querySelectorAll('.heart');
    const baskets = document.querySelectorAll('.buy-icon, .add-icon');
    const comments = document.querySelectorAll('.user-comment');

    const modal = document.getElementById('reviewModal');
    const span = document.querySelector('.close');
    var modalProductIdInput = document.getElementById("ModalProductId");
    var modalCommentIsNew = document.getElementById("ModalCommentIsNew");

    const textarea = modal.querySelector('textarea');

    var closeButton = document.getElementsByClassName("close")[0];


/*
    var priceRange = document.getElementById("priceRange");
    console.log("The HTML of the Price Range is " + priceRange);
    priceRange.addEventListener('change', (() => {
        console.log("Changed state of the Price");
        var price = document.getElementById('priceRange').value;
        document.getElementById('currentPrice').innerText = price;

        console.log("THe Price is: " + price);
    });

    function upatePrice() {
        var price = document.getElementById('priceRange').value;
        document.getElementById('currentPrice').innerText = price;
    }
*/

    closeButton.onclick = () => {
        modal.style.display = "none";
    }

    closeButton.addEventListener('click', () => {
        modal.style.display = 'none';
    });

    span.addEventListener('click', () => {
        modal.style.display = 'none';

    });

    window.addEventListener('click', event => {

        if (event.target == modal) {
            modal.style.display = 'none';
        };
    });

    const closeModalButton = document.querySelector('#reviewModal .close');
    closeModalButton.addEventListener('click', function () {
        modal.style.display = 'none';
    });



    const btnSubmit = document.querySelector('.submit-button');

    btnSubmit.addEventListener('click', () => {

        console.log("Clicked Submit Button");

        const reviewText = textarea.value;
        var productId = modalProductIdInput.value;

        let endpoint = (modalCommentIsNew.value == "true") ? '/Product/ModifyReview' : '/Product/GiveReview';

        fetch(endpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                review: reviewText,
                productId: Number(productId)
            })
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                console.log("The data is the following: ", data);
                modal.style.display = 'none';
            })
            .catch(error => {
                console.error("Error: ", error);
            });


        modal.style.display = 'none';

    });
    comments.forEach(comment => {

        comment.onclick = function (e) {
            modal.style.display = "block";
            modalProductIdInput.value = comment.getAttribute("data-product-id");
            modalCommentIsNew.value = comment.classList.contains("este_apasat");
            comment.classList.add("este_apasat");
        }

    });


    hearts.forEach(heart => {

        heart.addEventListener('click', () => {
            const productId = +heart.getAttribute('data-product-id');
            const userId = +heart.getAttribute('data-user-id');

            if (heart.classList.contains('far')) {

                heart.classList.remove('far');
                heart.classList.toggle('fas');

                fetch('/FavoriteProducts/AddFavorite', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userId: userId,
                        productId: productId
                    })
                })
                    .then(res => res.json())
                    .then(res => console.log(res))
                    .catch(error => console.error("Fetch Error: ", error));
            }

            else {

                heart.classList.remove('fas');
                heart.classList.toggle('far');

                fetch('/FavoriteProducts/DeleteFavorite', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userId: userId,
                        productId: productId
                    })
                })
                    .then(res => res.json())
                    .then(res => console.log(res))
                    .catch(error => console.error("Fetch Error: ", error));
            }

        });
    });

    baskets.forEach(basket => {

        basket.addEventListener('click', () => {

            const productId = +basket.getAttribute('data-product-id');
            const userId = +basket.getAttribute('data-user-id');

            if (basket.classList.contains("buy-icon")) {

                basket.classList.remove("buy-icon");
                basket.classList.add("add-icon");

               /* const response =*/ fetch('/UserBasket/AddProduct', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userId: userId,
                        productId: productId
                    })
                })
                    .then(res => res.json())
                    .then(res => console.log(res))
                    .catch(error => console.error("Fetch Error: ", error));

                var toastEl = document.getElementById('productToast');
                var toast = new bootstrap.Toast(toastEl);
                toast.show();
            }
            else if (basket.classList.contains("add-icon")) {

                const response = fetch('/UserBasket/AddProductQuantity', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userId: userId,
                        productId: productId
                    })
                })
                    .then(res => res.json())
                    .then(res => console.log(res))
                    .catch(error => console.error("Fetch Error: ", error));


                var toastEl = document.getElementById('productToast');
                var toast = new bootstrap.Toast(toastEl);
                toast.show();
            }
        });

        var CLOSEBUTTON = document.getElementsByClassName("close");

        CLOSEBUTTON.onclick = () => {
            modal.style.display = "none";
        }
    })

/*    let userRating; // Assume this is fetched from the server

    fetch(`/UserRating/GetUserRating?userId=${userId}&productId=${productId}`)
        .then(response => response.json())
        .then(data => {
            if (data) {
                userRating = data; // Save the rating in the variable
                updateStars(userRating);  // Call the updateStars function here
            } else {
                console.log("Rating not found");
            }
        })
        .catch(error => console.error("Fetch Error:", error));

*/


    const ratings = document.querySelectorAll('.rating');

    ratings.forEach(rating => {


        const stars = rating.querySelectorAll('.star');

        stars.forEach((star, index) => {
            var productId = +star.getAttribute('data-product-id');
            var userId = +star.getAttribute('data-user-id');
            var numberOfStars = 0;

            fetch(`/UserRating/GetUserRating?userId=${userId}&productId=${productId}`)
                .then(response => response.json())
                .then(data => {
                    if (data) {
                        console.log("User Rating:", data);
                    } else {
                        console.log("Rating not found");
                    }
                })
                .catch(error => console.error("Fetch Error:", error));



            star.addEventListener('click', () => {
                var productId = +rating.getAttribute('data-product-id');
                var userId = +rating.getAttribute('data-user-id');
                if (star.id == "one-star") {
                    console.log("one-star");
                    numberOfStars = 0;
                }
                else if (star.id == "two-stars") {
                    console.log("two-stars");
                    numberOfStars = 1;
                }
                else if (star.id == "three-stars") {
                    console.log("three-stars");
                    numberOfStars = 2;
                }
                else if (star.id == "four-stars") {
                    console.log("four-stars");
                    numberOfStars = 3;
                }
                else if (star.id == "five-stars") {
                    console.log("five-stars");
                    numberOfStars = 4;
                }

                fetch('/UserRating/AddRatingFromUser', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        userId: userId,
                        productId: productId,
                        numberOfStars: numberOfStars
                    })
                })
                    .then(res => res.json())
                    .then(res => console.log(res))
                    .catch(error => console.error("Fetch Error: ", error));

                console.log("Finished Giving Review To Product By User");


            })



            star.addEventListener('click', () => {
                for (let i = 0; i <= numberOfStars; i++) {
                    stars[i].classList.remove('far');
                    stars[i].classList.add('fas');
                }
                for (let i = numberOfStars + 1; i < 5; i++) {
                    stars[i].classList.remove('fas');
                    stars[i].classList.add('far');
                };
            });
        });
    });
});