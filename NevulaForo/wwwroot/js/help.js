document.addEventListener('DOMContentLoaded', function() {
  var chatContainer = document.querySelector('.chat-container');
  var chatToggleButton = document.querySelector('#chat-toggle-button');
  var closeButton = document.querySelector('#close-button');
  var sendButton = document.querySelector('#send-button');
  var messageInput = document.querySelector('#message-input');
  var chatMessages = document.querySelector('.chat-messages');

  chatToggleButton.addEventListener('click', function() {
    chatContainer.style.display = 'flex';
    chatToggleButton.style.display = 'none';
  });

  closeButton.addEventListener('click', function() {
    chatContainer.style.display = 'none';
    chatToggleButton.style.display = 'block';
  });

  this.documentElement.addEventListener('keydown', function(a) {
    if(a.keyCode === 27){
      chatContainer.style.display = 'none';
      chatToggleButton.style.display = 'block';
    }
  })

  this.documentElement.addEventListener('keydown', function(e){
    if(e.keyCode === 13){
    var message = messageInput.value.trim();
    if (message !== '') {
      var messageElement = document.createElement('div');
      messageElement.classList.add('message-client');
      messageElement.innerHTML = '<div class="message-content">' + message + '</div>';
      chatMessages.appendChild(messageElement);
      messageInput.value = '';
      chatMessages.scrollTop = chatMessages.scrollHeight;
    }
    }
  })

  sendButton.addEventListener('click', function() {
    var message = messageInput.value.trim();
    if (message !== '') {
      var messageElement = document.createElement('div');
      messageElement.classList.add('message-client');
      messageElement.innerHTML = '<div class="message-content">' + message + '</div>';
      chatMessages.appendChild(messageElement);
      messageInput.value = '';
      chatMessages.scrollTop = chatMessages.scrollHeight;
    }
  });
});

for(i = 1; i <= 8; i++){
  const question = document.getElementById('question-'+i);
  const answer = document.getElementById('answer-'+i);

  question.addEventListener('click', () => {
    question.classList.toggle('activo')
    answer.classList.toggle('activo')
  })
}