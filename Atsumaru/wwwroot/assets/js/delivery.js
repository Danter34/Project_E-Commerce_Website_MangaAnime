document.addEventListener('DOMContentLoaded', function() {
    // Checkbox Delivery Options
    const deliveryHomeCheckbox = document.querySelector('.chbox1 input[type="checkbox"]');
    const pickupStoreCheckbox = document.querySelector('.chbox2 input[type="checkbox"]');
    const addressDetails = document.querySelector('.deliver-box .add');
    const deliveryMethodsBox = document.querySelector('.dv-box');

    // Payment Options
    const momoCheckbox = document.querySelector('.chbox8 input[type="checkbox"]');
    const bankTransferCheckbox = document.querySelector('.chbox7 input[type="checkbox"]');
    const bankTransferModal = document.getElementById('bankTransferModal');
    const closeButton = document.querySelector('.close-button');

    // Shipping Cost Elements
    const shippingFeeSpan = document.getElementById('shipping-fee');
    const totalPriceSpan = document.getElementById('total-price');
    const shippingCheckboxes = document.querySelectorAll('.dv-box input[type="checkbox"]');

    // Hidden Inputs for MVC submission
    const selectedPaymentMethodInput = document.getElementById('selectedPaymentMethod');
    const selectedShippingMethodInput = document.getElementById('selectedShippingMethod');
    const checkoutForm = document.getElementById('checkoutForm');

    // **Giá trị tổng tiền hàng cố định (ví dụ) - Bạn cần lấy từ server hoặc cấu hình**
    let productTotal = 550000; // Ví dụ: Tổng tiền hàng là 550,000 VND

    // --- Initial Setup on Load ---
    // Mặc định, khi tải trang, "Giao hàng tận nơi" được chọn
    deliveryHomeCheckbox.checked = true;
    addressDetails.style.display = 'block';
    deliveryMethodsBox.style.display = 'block';
    
    // Đảm bảo không có phương thức vận chuyển nào được chọn mặc định
    shippingCheckboxes.forEach(cb => cb.checked = false);
    
    updateDisplayTotals(0); // Cập nhật hiển thị tổng tiền ban đầu với phí vận chuyển 0

    selectedPaymentMethodInput.value = '';
    selectedShippingMethodInput.value = '';

    // --- Event Listeners ---

    // Handle "Giao hàng tận nơi" checkbox
    deliveryHomeCheckbox.addEventListener('change', function() {
        if (this.checked) {
            pickupStoreCheckbox.checked = false;
            addressDetails.style.display = 'block';
            deliveryMethodsBox.style.display = 'block';
            
            shippingCheckboxes.forEach(cb => cb.checked = false);
            updateDisplayTotals(0); 
            selectedShippingMethodInput.value = '';
        } else {
            if (!pickupStoreCheckbox.checked) {
                addressDetails.style.display = 'none';
                deliveryMethodsBox.style.display = 'none';
                updateDisplayTotals(0);
                selectedShippingMethodInput.value = '';
            }
        }
    });

    // Handle "Nhận tại cửa hàng" checkbox
    pickupStoreCheckbox.addEventListener('change', function() {
        if (this.checked) {
            deliveryHomeCheckbox.checked = false;
            addressDetails.style.display = 'none';
            deliveryMethodsBox.style.display = 'none';
            updateDisplayTotals(0); // Shipping cost is 0 for pickup
            
            shippingCheckboxes.forEach(cb => cb.checked = false);
            selectedShippingMethodInput.value = 'StorePickup';
        } else {
            if (!deliveryHomeCheckbox.checked) {
                addressDetails.style.display = 'none';
                deliveryMethodsBox.style.display = 'none';
                updateDisplayTotals(0);
                selectedShippingMethodInput.value = '';
            }
        }
    });

    // Handle single selection for Shipping Methods
    shippingCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            if (this.checked) {
                shippingCheckboxes.forEach(otherCheckbox => {
                    if (otherCheckbox !== this) {
                        otherCheckbox.checked = false;
                    }
                });
                selectedShippingMethodInput.value = this.value;
            } else {
                let anyShippingChecked = false;
                shippingCheckboxes.forEach(cb => {
                    if (cb.checked) {
                        anyShippingChecked = true;
                    }
                });
                if (!anyShippingChecked) {
                    selectedShippingMethodInput.value = '';
                }
            }
            // Recalculate totals after shipping change
            let currentShippingCost = 0;
            if (selectedShippingMethodInput.value && selectedShippingMethodInput.value !== 'StorePickup') {
                const checkedShippingCheckbox = document.querySelector(`.dv-box input[value="${selectedShippingMethodInput.value}"]`);
                if (checkedShippingCheckbox) {
                    currentShippingCost = parseInt(checkedShippingCheckbox.dataset.shippingCost);
                }
            }
            updateDisplayTotals(currentShippingCost);
        });
    });

    // Handle single selection for Payment Methods
    const paymentCheckboxes = document.querySelectorAll('.pt-box input[type="checkbox"]');
    paymentCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            if (this.checked) {
                paymentCheckboxes.forEach(otherCheckbox => {
                    if (otherCheckbox !== this) {
                        otherCheckbox.checked = false;
                    }
                });
                selectedPaymentMethodInput.value = this.value;

                if (this.value === 'BankTransfer') {
                    bankTransferModal.style.display = 'flex';
                } else {
                    bankTransferModal.style.display = 'none';
                }
            } else {
                let anyPaymentChecked = false;
                paymentCheckboxes.forEach(cb => {
                    if (cb.checked) {
                        anyPaymentChecked = true;
                    }
                });
                if (!anyPaymentChecked) {
                    selectedPaymentMethodInput.value = '';
                    bankTransferModal.style.display = 'none';
                }
            }
        });
    });

    // Close button for modal
    closeButton.addEventListener('click', function() {
        bankTransferModal.style.display = 'none';
    });

    // Close modal when clicking outside of it
    window.addEventListener('click', function(event) {
        if (event.target == bankTransferModal) {
            bankTransferModal.style.display = 'none';
        }
    });

    // --- Helper Functions ---

    // Function to update overall cart totals (renamed from updateCartTotals for clarity)
    function updateDisplayTotals(shippingCost) {
        shippingFeeSpan.textContent = formatCurrency(shippingCost);
        const newTotal = productTotal + shippingCost;
        totalPriceSpan.textContent = formatCurrency(newTotal);
    }

    function formatCurrency(amount) {
        return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    }
});