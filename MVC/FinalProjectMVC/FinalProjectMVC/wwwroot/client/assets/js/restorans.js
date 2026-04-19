(() => {
        const header = document.querySelector('#qodef-page-header');
        const topArea = document.querySelector('#qodef-top-area');
        const videoPopup = document.querySelector('#videoPopupSection');
        const videoPopupOpen = document.querySelector('#videoOverlayPlay');
        const videoPopupClose = document.querySelector('#videoPopupClose');
        const videoPopupFrame = document.querySelector('#videoPopupFrame');

        if (!header) return;

        const updateHeader = () => {
            const isSticky = window.scrollY > 10;
            header.classList.toggle('qodef-header--sticky', isSticky);
            if (topArea) topArea.style.display = isSticky ? 'none' : '';
        };

        window.addEventListener('scroll', updateHeader, { passive: true });
        updateHeader();

        if (videoPopup && videoPopupOpen && videoPopupClose && videoPopupFrame) {
            const openPopup = () => {
                videoPopup.classList.add('open');
                videoPopup.setAttribute('aria-hidden', 'false');
                videoPopupFrame.src = 'https://www.youtube-nocookie.com/embed/aqz-KE-bpKQ?autoplay=1&rel=0';
                document.body.style.overflow = 'hidden';
            };

            const closePopup = () => {
                videoPopup.classList.remove('open');
                videoPopup.setAttribute('aria-hidden', 'true');
                videoPopupFrame.src = '';
                document.body.style.overflow = '';
            };

            videoPopupOpen.addEventListener('click', openPopup);
            videoPopupClose.addEventListener('click', closePopup);
            videoPopup.addEventListener('click', (event) => {
                if (event.target === videoPopup) closePopup();
            });
        }
    })();
