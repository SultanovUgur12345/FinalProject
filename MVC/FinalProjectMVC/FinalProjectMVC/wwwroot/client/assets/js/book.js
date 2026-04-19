let startDate = null;
        let endDate = null;
        let currentCalendarGroup = null;

        function formatDate(date) {
            return date.toLocaleDateString('en-GB', { day: '2-digit', month: 'short', year: 'numeric' });
        }

        function isPastDate(date) {
            const compareDate = new Date(date);
            compareDate.setHours(0, 0, 0, 0);

            const today = new Date();
            today.setHours(0, 0, 0, 0);

            return compareDate < today;
        }

        function createMonthCalendar(year, month) {
            const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            const daysInMonth = new Date(year, month + 1, 0).getDate();
            const firstDayIndex = new Date(year, month, 1).getDay();

            const monthDiv = document.createElement('div');
            monthDiv.className = 'month';

            const title = document.createElement('h4');
            title.innerText = (monthNames[month] + ' ' + year).toUpperCase();
            monthDiv.appendChild(title);

            const weekdays = document.createElement('div');
            weekdays.className = 'days';
            ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'].forEach((day) => {
                const dayEl = document.createElement('div');
                dayEl.innerText = day;
                weekdays.appendChild(dayEl);
            });
            monthDiv.appendChild(weekdays);

            const datesGrid = document.createElement('div');
            datesGrid.className = 'dates';

            for (let i = 0; i < firstDayIndex; i++) {
                const empty = document.createElement('div');
                datesGrid.appendChild(empty);
            }

            for (let d = 1; d <= daysInMonth; d++) {
                const dateEl = document.createElement('div');
                dateEl.innerText = d;
                const currentDate = new Date(year, month, d);

                if (isPastDate(currentDate)) {
                    dateEl.classList.add('disabled');
                } else {
                    dateEl.dataset.time = currentDate.getTime();
                    dateEl.onclick = function () { selectDate(currentDate); };
                }

                datesGrid.appendChild(dateEl);
            }

            monthDiv.appendChild(datesGrid);
            return monthDiv;
        }

        function renderCalendar() {
            const wrapper = document.getElementById('calendarWrapper');
            wrapper.innerHTML = '';
            const today = new Date();
            const currentYear = today.getFullYear();
            const currentMonth = today.getMonth();

            wrapper.appendChild(createMonthCalendar(currentYear, currentMonth));
            wrapper.appendChild(createMonthCalendar(currentYear, currentMonth + 1));
            updateCalendarHighlight();
        }

        function updateCalendarHighlight() {
            const allDates = document.querySelectorAll('.dates div[data-time]');
            allDates.forEach((el) => {
                el.classList.remove('selected', 'in-range');
                const time = parseInt(el.dataset.time, 10);
                const date = new Date(time);

                if (startDate && date.getTime() === startDate.getTime()) {
                    el.classList.add('selected');
                }
                if (endDate && date.getTime() === endDate.getTime()) {
                    el.classList.add('selected');
                }
                if (startDate && endDate && date > startDate && date < endDate) {
                    el.classList.add('in-range');
                }
            });

            if (startDate) {
                document.getElementById('checkinText').innerText = formatDate(startDate);
            }
            if (endDate) {
                document.getElementById('checkoutText').innerText = formatDate(endDate);
            }
        }

        function selectDate(date) {
            if (isPastDate(date)) {
                return;
            }

            if (currentCalendarGroup === 'checkin') {
                startDate = date;
                endDate = null;
                document.getElementById('checkinText').innerText = formatDate(date);
                document.getElementById('checkoutText').innerText = 'Select date';
                closeCalendar();
            } else if (currentCalendarGroup === 'checkout') {
                if (!startDate) {
                    startDate = date;
                    document.getElementById('checkinText').innerText = formatDate(date);
                    closeCalendar();
                } else if (date > startDate) {
                    endDate = date;
                    document.getElementById('checkoutText').innerText = formatDate(date);
                    closeCalendar();
                } else if (date < startDate) {
                    startDate = date;
                    endDate = null;
                    document.getElementById('checkinText').innerText = formatDate(date);
                    document.getElementById('checkoutText').innerText = 'Select date';
                    closeCalendar();
                }
            }
        }

        function openCalendar(type, element) {
            currentCalendarGroup = type;
            renderCalendar();

            const modal = document.getElementById('calendarModal');
            const rect = element.getBoundingClientRect();

            modal.style.display = 'block';
            modal.style.position = 'absolute';
            modal.style.top = rect.bottom + window.scrollY + 5 + 'px';
            modal.style.left = rect.left + window.scrollX + 'px';
        }

        function closeCalendar() {
            document.getElementById('calendarModal').style.display = 'none';
            currentCalendarGroup = null;
        }

        document.getElementById('submitBtn').addEventListener('click', function () {
            const fullName = document.getElementById('fullNameInput').value.trim();
            const email = document.getElementById('emailInput').value.trim();
            const phone = document.getElementById('phoneInput').value.trim();
            const checkin = document.getElementById('checkinText').innerText;
            const checkout = document.getElementById('checkoutText').innerText;
            const guests = document.getElementById('guestsSelect').value;

            if (!fullName || !email || !phone) {
                alert('Please fill Full Name, Email Address and Phone Number!');
                return;
            }

            if (checkin === 'Select date' || checkout === 'Select date') {
                alert('Please select check-in and check-out dates!');
                return;
            }

            alert('Reservation Request\n\nFull Name: ' + fullName + '\nEmail: ' + email + '\nPhone: ' + phone + '\nCheck-in: ' + checkin + '\nCheck-out: ' + checkout + '\nGuests: ' + guests + '\n\nOur team will contact you shortly!');
        });

        document.getElementById('checkinBox').addEventListener('click', function (e) {
            e.stopPropagation();
            openCalendar('checkin', this);
        });

        document.getElementById('checkoutBox').addEventListener('click', function (e) {
            e.stopPropagation();
            if (!startDate) {
                alert('Please select CHECK-IN date first!');
                return;
            }
            openCalendar('checkout', this);
        });

        document.addEventListener('click', function (e) {
            const modal = document.getElementById('calendarModal');
            const isClickInsideBox = e.target.closest('.booking-box');
            const isClickInsideModal = e.target.closest('.calendar-modal');

            if (!isClickInsideBox && !isClickInsideModal && modal.style.display === 'block') {
                closeCalendar();
            }
        });

        (() => {
            const header = document.querySelector('#qodef-page-header');
            const topArea = document.querySelector('#qodef-top-area');

            if (header) {
                const onScroll = () => {
                    const sticky = window.scrollY > 10;
                    header.classList.toggle('qodef-header--sticky', sticky);
                    if (topArea) {
                        topArea.style.display = sticky ? 'none' : '';
                    }
                };

                window.addEventListener('scroll', onScroll, { passive: true });
                onScroll();
            }
        })();
