document.addEventListener('DOMContentLoaded', function() {
  var chatContainer = document.querySelector('.chat-container');
  var chatToggleButton = document.querySelector('#chat-toggle-button');
  var closeButton = document.querySelector('#close-button');
  var sendButton = document.querySelector('#send-button');
  var messageInput = document.querySelector('#message-input');
  var chatMessages = document.querySelector('.chat-messages');

  chatToggleButton.addEventListener('click', function() {
    chatContainer.style.display = 'block';
    chatToggleButton.style.display = 'none';
  });

  closeButton.addEventListener('click', function() {
    chatContainer.style.display = 'none';
    chatToggleButton.style.display = 'block';
  });

  sendButton.addEventListener('click', function() {
    var message = messageInput.value.trim();
    if (message !== '') {
      var messageElement = document.createElement('div');
      messageElement.classList.add('message');
      messageElement.innerHTML = '<div class="message-sender">Usuario</div><div class="message-content">' + message + '</div>';
      chatMessages.appendChild(messageElement);
      messageInput.value = '';
      chatMessages.scrollTop = chatMessages.scrollHeight;
    }
  });
});