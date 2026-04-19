(() => {
        function initHeader() {
            const header = document.querySelector('#qodef-page-header');
            const topArea = document.querySelector('#qodef-top-area');
            if (!header) return;

            const update = () => {
                const isSticky = window.scrollY > 10;
                header.classList.toggle('qodef-header--sticky', isSticky);
                if (topArea) topArea.style.display = isSticky ? 'none' : '';
            };

            window.addEventListener('scroll', update, { passive: true });
            update();
        }

       function initAccordion() {
  const questions = document.querySelectorAll('.faq-question');

  questions.forEach(question => {
    question.addEventListener('click', () => {
      const answer = question.nextElementSibling;

      document.querySelectorAll('.faq-answer').forEach(item => {
        if (item !== answer) {
          item.style.maxHeight = null;
          item.parentElement.classList.remove('active');
        }
      });

      if (answer.style.maxHeight) {
        answer.style.maxHeight = null;
        question.parentElement.classList.remove('active');
      } else {
        answer.style.maxHeight = answer.scrollHeight + 'px';
        question.parentElement.classList.add('active');
      }
    });
  });
}

      function initPopup() {
  const popup = document.querySelector('#videoPopupSection');
  if (!popup) return;

  const toggle = (state) => {
    popup.classList.toggle('open', state);
    popup.setAttribute('aria-hidden', !state);
    document.body.style.overflow = state ? 'hidden' : '';
  };

  document.querySelector('#videoPopupOpen').onclick = () => toggle(true);
  document.querySelector('#videoPopupClose').onclick = () => toggle(false);
}

        initHeader();
        initAccordion();
        initPopup();

      
    })();
