


const sliderContainer = document.querySelector('.slides');
const pageContainer = document.querySelector('.cens');
const prevBtn = document.querySelector('.prevs');
const nextBtn = document.querySelector('.nexts');

const cardsPerRow = 5;
const rowsPerPage = 4;
const cardsPerPage = cardsPerRow * rowsPerPage;

const allCards = document.querySelectorAll('.cards');
const totalCards = allCards.length;
const totalPages = Math.ceil(totalCards / cardsPerPage);

let currentPage = 1;

function showPage(page) {
    currentPage = page;
    const start = (page - 1) * cardsPerPage;
    const end = start + cardsPerPage;

    allCards.forEach((card, index) => {
        if(index >= start && index < end) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });

    updatePageIndicators();
}

function updatePageIndicators() {
    // Xóa các span số trang hiện tại
    pageContainer.querySelectorAll('span.ds').forEach(span => span.remove());

    // Thêm lại span số trang tương ứng
    for(let i = 1; i <= totalPages; i++) {
        const span = document.createElement('span');
        span.classList.add('ds');
        if(i === currentPage) {
            span.classList.add('active'); // đổi màu violet
        }
        span.textContent = i;
        span.style.cursor = 'pointer';

        // Bấm vào số trang chuyển trang
        span.addEventListener('click', () => {
            showPage(i);
        });

        // Thêm span vào trước nút next
        pageContainer.insertBefore(span, nextBtn);
    }

    // Cập nhật trạng thái nút prev, next (disable khi đầu/cuối)
    prevBtn.disabled = (currentPage === 1);
    nextBtn.disabled = (currentPage === totalPages);

    // Tăng khoảng cách giữa nút prev và next dựa theo số trang hiện tại
    // Ví dụ: mỗi trang tăng thêm 15px margin
    prevBtn.style.marginRight = `${(currentPage - 1) * 15}px`;
    nextBtn.style.marginLeft = `${(currentPage - 1) * 15}px`;
}

// Nút prev giảm trang
prevBtn.addEventListener('click', () => {
    if(currentPage > 1) {
        showPage(currentPage - 1);
    }
});

// Nút next tăng trang
nextBtn.addEventListener('click', () => {
    if(currentPage < totalPages) {
        showPage(currentPage + 1);
    }
});

// Khởi tạo hiển thị trang đầu tiên
showPage(1);



  

  