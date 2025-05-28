function openLeftMenu() {
  document.getElementById("leftMenu").style.display = "block";
}

function closeLeftMenu() {
  document.getElementById("leftMenu").style.display = "none";
}

function openRightMenu() {
  document.getElementById("rightMenu").style.display = "block";
}

function closeRightMenu() {
  document.getElementById("rightMenu").style.display = "none";
}

let currentIndex = 0;
  let slides = document.querySelector(".slider1");
  let totalSlides = document.querySelectorAll(".slide1").length;
  let dots = document.querySelectorAll(".d");
  let autoSlideInterval;

  function updateSliderPosition() {
      slides.style.transform = `translateX(-${currentIndex * 100}%)`;
      dots.forEach(dot => dot.classList.remove("active"));
      dots[currentIndex].classList.add("active");
  }

  function changeSlide(direction) {
      currentIndex += direction;
      if (currentIndex >= totalSlides) currentIndex = 0;
      if (currentIndex < 0) currentIndex = totalSlides - 1;
      updateSliderPosition();
  }

  function goToSlide(index) {
      currentIndex = index;
      updateSliderPosition();
  }

  function startAutoSlide() {
      autoSlideInterval = setInterval(() => changeSlide(1), 3000);
  }

  function stopAutoSlide() {
      clearInterval(autoSlideInterval);
  }
  startAutoSlide();

  
// Lấy tất cả các slider
const sliders = document.querySelectorAll('.slider-contai');

sliders.forEach(slider => {
  const slide = slider.querySelector('.slide');
  const prev = slider.querySelector('.prev');
  const next = slider.querySelector('.next');

  let scrollAmount = 0;
  const cardWidth = 165; // Kích thước của mỗi card

  // Khi nhấn nút Next
  next.addEventListener('click', () => {
    scrollAmount += cardWidth;

    // Kiểm tra và đảm bảo không vượt quá giới hạn của slide
    if (scrollAmount >= slide.scrollWidth - slide.clientWidth) {
        scrollAmount = Math.max(0, scrollAmount - cardWidth);
    }
    slide.style.transform = `translateX(-${scrollAmount}px)`; // Dịch chuyển slider
  });

  // Khi nhấn nút Prev
  prev.addEventListener('click', () => {
    scrollAmount = Math.max(0, scrollAmount - cardWidth);
    slide.style.transform = `translateX(-${scrollAmount}px)`; // Dịch chuyển slider
  });
});