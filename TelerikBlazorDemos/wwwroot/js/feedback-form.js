"use strict";!function(){const t="dry-run",i={bottom:"140px",bounceAfter:null,expandAfterBounceDuration:1e5,expandedDuration:null,hiddenDuration:1500,animationDuration:"0.5s",animationDelay:"0.5s",hideAnimationDelay:"1s",kinveyAppKey:"kid_rJKE638xD",isHelpfulText:"Is this article helpful?",thankYouText:"Thank you for your feedback!",expirePeriodInDays:7,resetAnimationOnBlur:!1,saveCollapsedState:!1,collapsedStateDuration:18e5};var e="p-ff-";const n=e+"bounce",a=e+"expand",o=e+"expanded",s=e+"collapse",r=e+"hide",c=e+"hiden",d=e+"thanks",h=e+"raise",p=e+"raised",m=e+"collapse-anim",l=e+"bounce-anim",u=e+"hide-anim",C=e+"expand-anim",b=e+"expand-width-anim",w=e+"raise-anim",v=e+"show-comment-anim",x=e+"hide-comment-anim";class y extends HTMLElement{constructor(){super(),this._animationEnd=this._animationEnd.bind(this),this._iconClick=this._iconClick.bind(this),this._collapseClick=this._collapseClick.bind(this),this._yesClick=this._yesClick.bind(this),this._noClick=this._noClick.bind(this),this._resetAnimations=this._resetAnimations.bind(this),this._submitComment=this._submitComment.bind(this),this._skipComment=this._skipComment.bind(this),this._windowBlur=this._windowBlur.bind(this),this._windowFocus=this._windowFocus.bind(this)}disconnectedCallback(){this._detachEvents(),this.wrapper&&this.wrapper.removeEventListener("animationend",this._animationEnd),this.commentContainer&&this.commentContainer.removeEventListener("animationend",this._animationEnd),super.disconnectedCallback&&super.disconnectedCallback()}async connectedCallback(){this.setAttribute("role","complementary"),this.setAttribute("aria-label","feedback form"),this.attachShadow({mode:"open"}),this.reinit()}reinit(t){this.options=Object.assign({},i,t),this._resetAnimations(),this._detachEvents(),this.shadowRoot.innerHTML="",function(t){var e=localStorage.getItem(_());if(e){e=JSON.parse(e);if(e.url===window.location.href&&null!==e.vote)return function(t,e){e=t.getDate()+e,t=t.setDate(e);return new Date>=t}(new Date(e.date),t)}return 1}(this.options.expirePeriodInDays)&&(this.shadowRoot.appendChild((t=>{const e=document.createElement("template");return e.innerHTML=`
      <style>
        #wrapper {
          position: fixed;
          right: -202px;
          bottom: ${t.bottom||i.bottom};
          z-index: 5000;
        }

        #wrapper #feedback-container {
          padding: 0 0 0 10px;
          border-radius: 999999px 0 0 999999px;
          border: 2px solid;
          border-right: 0;
          display: flex;
          gap: 12px;
          align-items: center;
          height: 70px;
          width: 266px;
        }

        #wrapper #comment-container {
          position: absolute;
          width: 248px;
          top: 35px;
          padding: 48px 14px 16px 16px;
          z-index: -1;
          opacity: 0;
          visibility: hidden;
        }

        #wrapper.${p} #comment-container {
          opacity: 1;
          visibility:visible;
        }

        #wrapper.${h}-reverse #comment-container {
          opacity: 0;
          visibility: hidden;
        }

        #wrapper #comment-container #comment {
          width: 234px;
          height: 72px;
          resize: none;
          margin-bottom: 10px;
          padding: 6px 8px;
        }

        #buttons-container {
          display: flex;
          gap: 32px;
        }

        #comment-buttons-container {
          display: flex;
          gap: 20px;
        }

        #wrapper button {
          background-color: transparent;
          border: none;
          padding:0;
          margin:0;
          cursor: pointer;
        }

        #wrapper button svg {
          vertical-align: middle;
        }

        #wrapper.${o} {
          right: 0;
        }

        #wrapper.${c} {
          right: -300px;
        }

        #wrapper.${p} {
          bottom: ${parseInt(t.bottom||i.bottom,10)+20}px;
        }

        #wrapper.${o} #icon {
          cursor: default;
        }

        #yes-btn.clicked {
          color: #10975A !important;
          fill: #10975A !important;
        }

        #yes-btn:not(.clicked) .fill,
        #no-btn:not(.clicked) .fill {
          display: none;
        }

        #yes-btn.clicked .outline,
        #no-btn.clicked .outline {
          display: none;
        }

        #no-btn.clicked {
          color: #F02727 !important;
          fill: #F02727 !important;;
        }

        #wrapper.${o} #expanded-container,
        #wrapper.${a} #expanded-container {
          display: block;
        }

        #icon {
          width: 50px;
          height: 50px;
          border-radius: 999999px;
          cursor: pointer;
        }

        #icon img {
          width: 50px;
          height: 50px;
        }

        #expanded-container {
          opacity: 1;
          display: none;
          flex-direction: column;
        }

        #text-container {
          padding-bottom: 8px;
          display: flex;
          gap:40px;
        }

        #expanded-container #text-container #thankyou-text {
          position: absolute;
          opacity: 0;
          visibility: hidden;
        }

        #expanded-container.${d} #collapse-btn,
        #wrapper.${p} #collapse-btn {
          display: none;
        }

        #expanded-container.${d} #buttons-container,
        #wrapper.${h} #collapse-btn,
        #wrapper.${s} #expanded-container {
          transition: visibility ${t.animationDuration}, opacity ${t.animationDuration} linear;
          visibility: hidden;
          opacity: 0;
        }

        #expanded-container.${d} #text-container {
          transition: margin ${t.animationDuration} linear ${t.animationDelay};
          margin-top: 25px;
        }

        #expanded-container.${d} #text-container #text {
          transition: all ${t.animationDuration} linear ${t.animationDelay};
          opacity: 0;
        }

        #expanded-container.${d} #text-container #thankyou-text {
          transition: all ${t.animationDuration} linear ${t.animationDelay};
          opacity: 1;
          visibility: visible;
        }

        @keyframes ${l} {
          0% {right: -202px;}
          50% {right: -192px;}
          100% {right: -300px;}
        }

        @keyframes ${m} {
          70% {right: -242px;}
          100% {right: -202px;}
        }

        @keyframes ${m}-reverse {
          70% {right: -182px;}
          100% {right: -202px;}
        }

        @keyframes ${u} {
          to {right: -300px;}
        }

        @keyframes ${C} {
          to {right: 0;}
        }

        @keyframes ${b} {
          50% {width: 350px;}
          100% {width: 266px;}
        }

        @keyframes ${v} {
          100% {opacity: 1;}
        }

        @keyframes ${x} {
          100% {opacity: 0;}
        }

        @keyframes ${w} {
          to {bottom: ${parseInt(t.bottom||i.bottom,10)+20}px;}
        }

        @keyframes ${w}-reverse {
          to {bottom: ${t.bottom||i.bottom};}
        }

        #wrapper.${n} {
          animation-name: ${l};
          animation-duration: ${t.animationDuration};
        }

        #wrapper.${s} {
          animation-name: ${m};
          animation-duration: ${t.animationDuration};
        }

        #wrapper.${s}-reverse {
          animation-name: ${m}-reverse;
          animation-duration: ${t.animationDuration};
        }

        #wrapper.${r} {
          animation-name: ${u};
          animation-duration: ${t.animationDuration};
          animation-delay: ${t.hideAnimationDelay};
        }

        #wrapper.${a} {
          animation-name: ${C};
          animation-duration: ${t.animationDuration};
        }

        #wrapper.${a} #feedback-container {
          animation-name: ${b};
          animation-duration: ${t.animationDuration};
        }

        #wrapper.${h} {
          animation-name: ${w};
          animation-duration: ${t.animationDuration};
          animation-delay: ${t.animationDelay};
        }

        #wrapper.${h} #comment-container {
          animation-name: ${v};
          animation-duration: ${t.animationDuration};
          animation-delay: ${t.animationDelay};
        }

        #wrapper.${h}-reverse {
          animation-name: ${w}-reverse;
          animation-duration: ${t.animationDuration};
          animation-delay: ${t.animationDelay};
        }

        #wrapper.${h}-reverse #comment-container {
          animation-name: ${x};
          animation-duration: ${t.animationDuration};
        }
      </style>
      <div id="wrapper" part="main">
        <div id="feedback-container" part="feedback">
          <div id="icon" tabindex="0" aria-label="Open feedback form" role="button">
            ${t.iconUrl?`<img src="${t.iconUrl}" alt="Feedback form product icon" />`:"<slot></slot>"}
          </div>
          <div id="expanded-container" part="expanded-container">
            <div id="text-container" part="text">
              <span id="text" tabindex="-1">${t.questionMsg||i.isHelpfulText}</span>
              <span id="thankyou-text">${t.thankYouText}</span>
              <button id="collapse-btn" aria-label="Collapse feedback form" tabindex="-1">
                <svg width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg">
                  <path d="M7.71875 3.10938L7.49687 3.33125C7.35 3.47812 7.35 3.71562 7.49687 3.8625L11.1219 7.46875H1.375C1.16875 7.46875 1 7.6375 1 7.84375V8.15625C1 8.3625 1.16875 8.53125 1.375 8.53125H11.1219L7.5 12.1375C7.35313 12.2844 7.35313 12.5219 7.5 12.6687L7.72188 12.8906C7.86875 13.0375 8.10625 13.0375 8.25313 12.8906L12.8938 8.26562C13.0406 8.11875 13.0406 7.88125 12.8938 7.73438L8.25 3.10938C8.10313 2.9625 7.86562 2.9625 7.71875 3.10938ZM15 2.375V13.625C15 13.8313 14.8313 14 14.625 14H14.375C14.1687 14 14 13.8313 14 13.625V2.375C14 2.16875 14.1687 2 14.375 2H14.625C14.8313 2 15 2.16875 15 2.375Z"/>
                </svg>
              </button>
            </div>
            <div id="buttons-container">
              <button id="yes-btn" part="button" tabindex="-1">
                <svg class="outline" width="16" height="16" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg">
    <path d="M31.041 17.8552C31.6614 17.0506 32 16 32 14.7168C31.9999 12.3621 29.9954 10.1811 27.4546 10.1811H23.0702C23.6152 9.09556 24.3636 7.74913 24.3636 5.8175C24.3637 2.167 22.9074 0 19.1819 0C17.3072 0 16.6115 2.36987 16.2551 4.20175C16.0421 5.29619 15.8409 6.33 15.2222 6.94869C13.7491 8.42188 11.5 12 10.1489 12.7266C10.0121 12.7842 9.83725 12.8296 9.64688 12.8655C9.28606 12.3428 8.68319 12 8 12H2C0.895437 12 0 12.8954 0 14V30C0 31.1046 0.895437 32 2 32H8C9.10456 32 10 31.1046 10 30V29.4538C12.0309 29.4538 16.2929 32.0004 21.0909 31.9991C21.435 31.9993 23.4441 32.001 23.6542 31.9991C27.3594 32 29.4202 29.7562 29.3002 26.3871C30.2456 25.2791 30.7089 23.6785 30.4393 22.193C31.2178 20.9726 31.3846 19.2511 31.041 17.8552ZM2 30V14H8V30H2ZM28.5011 17.272C29.5 18 29.5 21 28.1506 21.6886C28.9958 23.1111 28.2351 25.0146 27.2122 25.5633C27.7305 28.8471 26.0271 29.9762 23.6364 29.9992C23.4294 30.0011 21.3085 29.9992 21.091 29.9992C16.5329 29.9992 12.6696 27.4538 10.0001 27.4538V14.7266C12.3568 14.7266 14.5191 10.4804 16.6365 8.36294C18.5456 6.45388 17.9092 3.27206 19.1819 1.99937C22.3638 1.99937 22.3638 4.21919 22.3638 5.81756C22.3638 8.45438 20.4547 9.63569 20.4547 12.1811H27.4546C28.8752 12.1811 29.9938 13.4539 30.0001 14.7266C30.0063 15.9993 29.5 17 28.5011 17.272ZM6.5 27C6.5 27.8284 5.82844 28.5 5 28.5C4.17156 28.5 3.5 27.8284 3.5 27C3.5 26.1716 4.17156 25.5 5 25.5C5.82844 25.5 6.5 26.1716 6.5 27Z"/>
  </svg>
  <svg class="fill" width="16" height="16" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg">
    <path d="M6.5 14H1.5C0.671562 14 0 14.6716 0 15.5V30.5C0 31.3284 0.671562 32 1.5 32H6.5C7.32844 32 8 31.3284 8 30.5V15.5C8 14.6716 7.32844 14 6.5 14ZM4 29.5C3.17156 29.5 2.5 28.8284 2.5 28C2.5 27.1716 3.17156 26.5 4 26.5C4.82844 26.5 5.5 27.1716 5.5 28C5.5 28.8284 4.82844 29.5 4 29.5ZM24 5.09075C24 7.74175 22.3769 9.22875 21.9202 11H28.2779C30.3652 11 31.9902 12.7341 31.9999 14.6311C32.0052 15.7523 31.5283 16.9592 30.785 17.7059L30.7781 17.7128C31.3929 19.1714 31.2929 21.2151 30.1964 22.6796C30.7389 24.2981 30.1921 26.2861 29.1725 27.3519C29.4411 28.4518 29.3127 29.3879 28.7882 30.1414C27.5126 31.9742 24.351 32 21.6774 32L21.4996 31.9999C18.4817 31.9989 16.0118 30.9001 14.0271 30.0171C13.0298 29.5734 11.7258 29.0242 10.7364 29.006C10.3277 28.9985 10 28.6649 10 28.2561V14.8955C10 14.6955 10.0801 14.5036 10.2224 14.3629C12.6982 11.9164 13.7629 9.32625 15.7922 7.2935C16.7174 6.3665 17.0539 4.96625 17.3792 3.61212C17.6572 2.45581 18.2386 0 19.5 0C21 0 24 0.5 24 5.09075Z" />
  </svg>
                Yes
              </button>
              <button id="no-btn" part="button" tabindex="-1">
                <svg class="outline" width="16" height="16" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg">
    <path d="M31.041 14.1448C31.3846 12.7488 31.2177 11.0273 30.4393 9.80697C30.7089 8.32147 30.2456 6.72091 29.3002 5.61291C29.4202 2.24385 27.3594 -2.89308e-05 23.6542 0.000846069C23.4441 -0.00102893 21.435 0.000658569 21.0909 0.000846069C16.2871 -0.000466431 12.2448 2.49997 10 2.49997H9.32219C8.96969 2.18903 8.50706 1.99997 8 1.99997H2C0.895437 1.99997 0 2.89541 0 3.99997V20C0 21.1045 0.895437 22 2 22H8C9.10456 22 10 21.1045 10 20V19.2199C10.0531 19.2365 10.1033 19.2542 10.1489 19.2734C11.5 20 13.7491 23.5781 15.2222 25.0512C15.841 25.67 16.0421 26.7037 16.2551 27.7982C16.6115 29.6301 17.3072 32 19.1819 32C22.9074 32 24.3637 29.833 24.3637 26.1825C24.3637 24.2508 23.6152 22.9044 23.0702 21.8189H27.4546C29.9955 21.8189 32 19.6379 32.0001 17.2832C32 16 31.6614 14.9494 31.041 14.1448ZM8 20H2V3.99997H8V20ZM27.4546 19.8188H20.4546C20.4546 22.3643 22.3637 23.5456 22.3637 26.1824C22.3637 27.7808 22.3637 30.0006 19.1819 30.0006C17.9092 28.7278 18.5455 25.5461 16.6364 23.637C14.8485 21.8491 12.1819 17.2734 10.2728 17.2734H10V4.54622H10.2728C12.4416 4.54622 16.6364 2.00078 21.0909 2.00078C21.3084 2.00078 23.4294 1.99891 23.6364 2.00078C26.0271 2.02378 27.7304 3.15291 27.2121 6.43666C28.2351 6.98541 28.9958 8.88885 28.1506 10.3114C29.5 11 29.5 14 28.5011 14.728C29.5 15 30.0063 16.0007 30 17.2734C29.9937 18.5462 28.8752 19.8188 27.4546 19.8188ZM6.5 17C6.5 17.8284 5.82844 18.5 5 18.5C4.17156 18.5 3.5 17.8284 3.5 17C3.5 16.1715 4.17156 15.5 5 15.5C5.82844 15.5 6.5 16.1715 6.5 17Z" />
  </svg>
  <svg class="fill" width="16" height="16" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg">
      <path d="M0 3.5V18.5C0 19.3284 0.671562 20 1.5 20H6.5C7.32844 20 8 19.3284 8 18.5V3.5C8 2.67156 7.32844 2 6.5 2H1.5C0.671562 2 0 2.67156 0 3.5ZM2.5 16C2.5 15.1716 3.17156 14.5 4 14.5C4.82844 14.5 5.5 15.1716 5.5 16C5.5 16.8284 4.82844 17.5 4 17.5C3.17156 17.5 2.5 16.8284 2.5 16ZM19.5 32C18.2386 32 17.6572 29.5442 17.3793 28.3878C17.0539 27.0337 16.7175 25.6334 15.7922 24.7064C13.7629 22.6737 12.6983 20.0835 10.2224 17.637C10.152 17.5674 10.096 17.4844 10.0579 17.393C10.0197 17.3016 10.0001 17.2035 10.0001 17.1044V3.74381C10.0001 3.335 10.3278 3.00144 10.7365 2.99394C11.7259 2.97581 13.0299 2.4265 14.0272 1.98281C16.0118 1.09987 18.4818 0.0010625 21.4997 0H21.6774C24.351 0 27.5126 0.0258125 28.7883 1.85856C29.3128 2.61212 29.4412 3.54819 29.1726 4.64806C30.1921 5.71388 30.739 7.702 30.1964 9.32038C31.2929 10.7849 31.3929 12.8286 30.7782 14.2872L30.7851 14.2941C31.5284 15.0409 32.0052 16.2478 32 17.3689C31.9902 19.2659 30.3652 21 28.2779 21H21.9202C22.3769 22.7712 24 24.2583 24 26.9093C24 31.5 21 32 19.5 32Z"/>
  </svg>
              No</button>
            </div>
          </div>
        </div>
        <div id="comment-container" part="comment-area">
          <textarea id="comment" part="textarea" placeholder="How can we improve this article?" tabindex="-1"></textarea>
          <div id="comment-buttons-container">
            <button id="submit-comment" part="submit-button" tabindex="-1">Submit comment</button>
            <button id="skip-comment" part="comment-button" tabindex="-1">Skip comment</button>
          </div>
        </div>
      </div>`,e.content.cloneNode(!0)})(Object.assign({bottom:this.bottom,iconUrl:this.iconUrl,questionMsg:this.questionMsg},this.options))),this.wrapper=this.shadowRoot.querySelector("#wrapper"),this.icon=this.shadowRoot.querySelector("#icon"),this.collapseBtn=this.shadowRoot.querySelector("#collapse-btn"),this.yesBtn=this.shadowRoot.querySelector("#yes-btn"),this.noBtn=this.shadowRoot.querySelector("#no-btn"),this.text=this.shadowRoot.querySelector("#text"),this.expandedContainer=this.shadowRoot.querySelector("#expanded-container"),this.commentContainer=this.shadowRoot.querySelector("#comment-container"),this.commentText=this.shadowRoot.querySelector("#comment"),this.submitCommentBtn=this.shadowRoot.querySelector("#submit-comment"),this.skipCommentBtn=this.shadowRoot.querySelector("#skip-comment"),this._attachEvents(),function(t){var e=localStorage.getItem(k());if(e){const i=new Date(e),n=new Date;e=n.getTime()-i.getTime();return t<e}return 1}(this.options.collapsedStateDuration)&&(this._actionToReset=this._animationAction({action:this._bounce,timeout:this.options.bounceAfter,fallBack:{action:this._expand,params:[!1],timeout:this.options.expandAfterBounceDuration}})))}get kinveyAppKey(){return this.getAttribute("kinvey-app-key")||this.options.kinveyAppKey}get sheetId(){return this.getAttribute("sheet-id")}get bottom(){return this.getAttribute("bottom")}get iconUrl(){return this.getAttribute("icon-url")}get expanded(){return this.wrapper.classList.contains(o)}get raised(){return this.wrapper.classList.contains(p)}get questionMsg(){return this.getAttribute("question-msg")}get isDryRun(){return"true"===this.getAttribute(t)||"True"===this.getAttribute(t)||"TRUE"===this.getAttribute(t)}_animationAction(t){const{action:e,timeout:i,fallBack:n,params:a}=t;return e&&i?setTimeout(()=>{e.apply(this,a)},i):n?this._animationAction(n):null}_attachEvents(){this.icon.addEventListener("click",this._iconClick),this.icon.addEventListener("keydown",this._iconClick),this.collapseBtn.addEventListener("click",this._collapseClick),this.yesBtn.addEventListener("click",this._yesClick),this.noBtn.addEventListener("click",this._noClick),this.submitCommentBtn.addEventListener("click",this._submitComment),this.skipCommentBtn.addEventListener("click",this._skipComment),this.wrapper.addEventListener("animationend",this._animationEnd),this.commentContainer.addEventListener("animationend",this._animationEnd),this.options.resetAnimationOnBlur&&(window.addEventListener("blur",this._windowBlur),window.addEventListener("focus",this._windowFocus)),this._resetAnimOnFocus=()=>{this._resetAnimations(),this.removeEventListener("keyup",this._resetAnimOnFocus)},this.addEventListener("keyup",this._resetAnimOnFocus)}_detachEvents(){this.icon&&(this.icon.removeEventListener("click",this._iconClick),this.icon.removeEventListener("keydown",this._iconClick),this.yesBtn.removeEventListener("click",this._yesClick),this.noBtn.removeEventListener("click",this._noClick),this.submitCommentBtn.removeEventListener("click",this._submitComment),this.skipCommentBtn.removeEventListener("click",this._skipComment),this.removeEventListener("keyup",this._resetAnimOnFocus),window.removeEventListener("blur",this._windowBlur),window.removeEventListener("focus",this._windowFocus))}_windowBlur(){this._resetAnimations()}_windowFocus(){this._resetAnimations(),this._animationPlayed||(this._actionToReset=this._animationAction({action:this._bounce,timeout:this.options.bounceAfter,fallBack:{action:this._expand,params:[!1],timeout:this.options.expandAfterBounceDuration}}))}_isKeyDown(t){return"keydown"===t.type||"click"===t.type&&!t.clientX&&!t.clientY}_iconClick(t){this._isKeyDown(t)&&13!==t.keyCode&&32!==t.keyCode||(this._isKeyDown(t)&&this._focus(this.text),this._resetAnimations(),this.expanded||this._expand())}_collapseClick(t){this._isKeyDown(t)&&this._focus(this.icon),this._resetAnimations(),this._collapse(!1,!1),this.options.saveCollapsedState&&localStorage.setItem(k(),new Date)}_yesClick(){this._resetAnimations(),this.noBtn.classList.remove("clicked"),this.yesBtn.classList.add("clicked"),this._vote="yes",this._thankYou()}_noClick(t){this._tabIndex([this.commentText,this.submitCommentBtn,this.skipCommentBtn],0),this._tabIndex([this.collapseBtn],-1),this._isKeyDown(t)&&this._focus(this.commentText),this._resetAnimations(),this.yesBtn.classList.remove("clicked"),this.noBtn.classList.add("clicked"),this.wrapper.classList.add(h),this._vote="no"}_thankYou(){if(!this.isDryRun){i=this._gatherData(),t=`https://baas.kinvey.com/rpc/${i.kinveyAppKey}/custom/saveFeedback`;var e=i.vote;e={date:new Date,vote:e,url:window.location.href},localStorage.setItem(_(),JSON.stringify(e));{e=i.vote;let t=window.dataLayer||[];t.push({event:"virtualEvent",eventCategory:"feedback",eventAction:function(t){if(!t)return"";var e=t.toUpperCase()[0],t=t.toLowerCase().slice(1);return e+t}(e),eventLabel:window.location.href})}i.submitToKinvey&&i.comment&&fetch(t,{method:"POST",body:JSON.stringify(function(t){return{path:window.location.href,productId:t.sheetId,sheetId:t.sheetId,text:t.comment,textFeedback:t.comment,uuid:function(t){t=document.cookie.match(new RegExp(t+"=([^;]+)"));if(t)return t[1]}("uuid"),acceptFeedbackContact:!1,email:"",hasPreviousFeedback:"false",inaccurateContent:!1,inaccurateOutdatedContentText:"",otherFeedback:""!==t.comment,otherMoreInformation:!1,otherMoreInformationText:"",textErrors:!1,typosLinksElementsText:"",outdatedSample:!1,inaccurateOutdatedCodeSamplesText:"",yesNoFeedback:"Not submitted"}}(i)),headers:{"Content-type":"application/json; charset=UTF-8",Authorization:"Basic "+window.btoa("feedback:feedback","base64")}})}var i,t;this._detachEvents(),this.expandedContainer.classList.add(d),this.raised&&this.wrapper.classList.add(h+"-reverse"),this._hide(),this.setAttribute("aria-hidden",!0)}_gatherData(){return{sheetId:this.sheetId,kinveyAppKey:this.kinveyAppKey,vote:this._vote,comment:this._comment||"",submitToKinvey:this._submitToKinvey}}_resetAnimations(){window.clearTimeout(this._actionToReset)}_submitComment(){this._comment=this.commentText.value,this._submitToKinvey=!0,this._thankYou()}_skipComment(){this._submitToKinvey=!1,this._thankYou()}_bounce(){this._animationPlayed=!1,this.wrapper.classList.add(n),this._actionToReset=this._animationAction({action:this._collapse,params:[!0,!0],timeout:this.options.hiddenDuration})}_hide(){this.wrapper.classList.add(r)}_collapse(t,e){this._tabIndex([this.collapseBtn,this.yesBtn,this.noBtn,this.text],-1),this._tabIndex([this.icon],0);e=e?"-reverse":"";this.wrapper.classList.add(s+e),t?this._actionToReset=this._animationAction({action:this._expand,params:[!0],timeout:this.options.expandAfterBounceDuration}):this._animationPlayed=!0}_expand(t){this._tabIndex([this.collapseBtn,this.yesBtn,this.noBtn,this.text],0),this._tabIndex([this.icon],-1),this.wrapper.classList.add(a),t&&(this._actionToReset=this._animationAction({action:this._collapse,params:[!1],timeout:this.options.expandedDuration}))}_tabIndex(t,e){t.forEach(t=>t.setAttribute("tabindex",e))}_focus(t){setTimeout(()=>{t.focus()},0)}_animationEnd(t){switch(t.animationName){case l:this.wrapper.classList.remove(n),this.wrapper.classList.add(c);break;case u:this.wrapper.classList.remove(o),this.wrapper.classList.remove(r),this.wrapper.classList.add(c);break;case m:case m+"-reverse":this.wrapper.classList.remove(c),this.wrapper.classList.remove(o),this.wrapper.classList.remove(s),this.wrapper.classList.remove(s+"-reverse");break;case C:this.wrapper.classList.remove(c),this.wrapper.classList.remove(a),this.wrapper.classList.add(o);break;case w:this.wrapper.classList.remove(h),this.wrapper.classList.add(p);break;case w+"-reverse":this.wrapper.classList.remove(p),this.wrapper.classList.remove(h+"-reverse")}}}function _(){return"T_DOCUMENTATION_FEEDBACK_SUBMIT"+window.location.href}function k(){return"T_DOCUMENTATION_FEEDBACK_COLLAPSED"}"customElements"in window?customElements.define("feedback-form",y):document.addEventListener("DOMContentLoaded",function(){let t=document.querySelector("feedback-form");t.parentNode.removeChild(t)})}();