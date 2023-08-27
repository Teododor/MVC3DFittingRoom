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

    closeButton.onclick = () => {
        modal.style.display = "none";
        console.log("CLICKED THE CLOSE BUTTON");
    }

    closeButton.addEventListener('click', () => {
        modal.style.display = 'none';
    });

    span.addEventListener('click', () => {
        console.log("Clicked the span with queryselector .close");
        modal.style.display = 'none';
/*
        let textCamp = document.getElementById("WrittenTextArea");
        textCamp.value = "";
        textCamp.innerHTML = "";*/

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

        const reviewText = textarea.value;
        var productId = modalProductIdInput.value;

        //TBD : CHESTIA ASTA SE VA MODIFICA CA SA NU VERIFIC DE FIECARE DATA MODALCOMMENTISNEW.VALUE == "TRUE" SI IL VOI DA CA BOOOLEAN

        let endpoint = (modalCommentIsNew.value == "true") ?  '/Product/ModifyReview' : '/Product/GiveReview' ;


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

        console.log("Entered Hearts forEach");

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

        console.log("Entered Products forEach");


        basket.addEventListener('click', () => {

            const productId = basket.getAttribute('data-product-id');
            const userId = basket.getAttribute('data-user-id');

            if (basket.classList.contains("buy-icon")) {

                basket.classList.remove("buy-icon");
                basket.classList.add("add-icon");

                const response = fetch('/UserBasket/AddProduct', {
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
            console.log("CLICKED THE CLOSE BUTTON");
        }
    })


    const ratings = document.querySelectorAll('.rating');

    ratings.forEach(rating => {
        const productId = rating.getAttribute('data-product-id');
        const userId = rating.getAttribute('data-user-id');

        const stars = rating.querySelectorAll('.star');
        console.log("Stars");
        console.log(stars);

        stars.forEach((star, index) => {

            /*            var numberOfStars = 0;
            
                        console.log("Star " + star);
                        if (star.id == "one-star") {
                            console.log("one-star");
                            numberOfStars = 1;
                        }
                        else if (star.id == "two-stars") {
                            console.log("two-stars");
                            numberOfStars = 2;
                        }
                        else if (star.id == "three-stars") {
                            console.log("three-stars");
                            numberOfStars = 3;
                        }
                        else if (star.id == "four-stars") {
                            console.log("four-stars");
                            numberOfStars = 4;
                        }
                        else if (star.id == "five-stars") {
                            console.log("five-stars");
                            numberOfStars = 5;
                        }
            
            
                        console.log("The Product has " + numberOfStars + " STARS");*/



            star.addEventListener('click', () => {
                for (let i = 0; i <= index; i++) {
                    stars[i].classList.remove('far');
                    stars[i].classList.add('fas');
                }
                for (let i = index + 1; i < 5; i++) {
                    stars[i].classList.remove('fas');
                    stars[i].classList.add('far');
                };
            });
        });
    });
});