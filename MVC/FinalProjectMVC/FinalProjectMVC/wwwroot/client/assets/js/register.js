// Sticky header color change on scroll
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

        // Toggle password visibility
        function togglePassword(inputId, element) {
            const input = document.getElementById(inputId);
            const icon = element.querySelector('i');
            
            if (input.type === 'password') {
                input.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                input.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        }
