
let categoryArchiveBtns = document.querySelectorAll(".add-archive");
categoryArchiveBtns.forEach(item =>
    item.addEventListener("click", function () {
        alert("s")
        let id = parseInt(this.getAttribute("data-id"));
        //  fetch("category/settoarchive", {
        //      method: 'POST',
        //      headers: {
        //          "Content-type": "application/x-www-form-urlencoded; charset=UTF-8"
        //  },
        //      credentials: 'include',
        //      body: id
        //})
        //      .then(res.json())
        //      .then(res => {

        //          console.log('Response: ', res);
        //      })

        (async () => {
            const rawResponse = await fetch(`admin/category/settoarchive?id=${id}`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            });
            const content = await rawResponse.json();

            console.log(this.closest(".category-data"));
        })();

    })