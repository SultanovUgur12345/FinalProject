(() => {
            const header = document.querySelector('#qodef-page-header');
            const topArea = document.querySelector('#qodef-top-area');

            if (header) {
                const onScroll = () => {
                    const sticky = window.scrollY > 10;
                    header.classList.toggle('qodef-header--sticky', sticky);
                    if (topArea) topArea.style.display = sticky ? 'none' : '';
                };

                window.addEventListener('scroll', onScroll, { passive: true });
                onScroll();
            }
        })();
