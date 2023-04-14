var container = document.querySelector('#unity-container')
var canvas = document.querySelector('#unity-canvas')
var loadingBar = document.querySelector('#loader')
var progressBarFull = document.querySelector('#unity-progress-bar-full')
var fullscreenButton = document.querySelector('#unity-fullscreen-button')

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
  container.className = 'unity-mobile'
  config.devicePixelRatio = 1
} else {
  let h = document.documentElement.clientHeight
  let w = document.documentElement.clientWidth
  canvas.style.width = h * 1.7 + 'px'
  canvas.style.height = h + 'px'
  // canvas.style.width = '100vw'
  // canvas.style.height = '100vh'
}

var script = document.createElement('script')
script.src = loaderUrl
script.onload = () => {
  createUnityInstance(canvas, config, (progress) => {
    const progress2 = document.querySelector('#loader .progress')
    progress2.style.display = 'block'
    loader.querySelector('.spinner').style.display = 'none'
    let percentage = parseInt(progress * 1000) / 10
    if (percentage >= 85) {
      document.getElementById('percentage').innerHTML =
        '下载完成，请稍等正在加载资源包...'
    } else {
      document.getElementById('percentage').innerHTML = percentage + '%'
    }
    document.querySelector('#loader .progress .full').style.transform =
      'scaleX(' + progress + ')'

    if (progress == 1) {
      removeTimeout = setTimeout(function () {
        loader.style.display = 'none'
      }, 0)
    }
  })
    .then((unityInstance) => {
      window.gameInstance = unityInstance
      loadingBar.style.display = 'none'
    })
    .catch((message) => {
      alert(message)
    })
}
document.body.appendChild(script)

function getQueryVariable(variable) {
  var query = window.location.search.substring(1)
  var vars = query.split('&')
  if (variable == 'access_token' && vars[3]) {
    var pair = vars[3].split('access_token=')
    return pair[1]
  }
  for (var i = 0; i < vars.length; i++) {
    var pair = vars[i].split('=')
    if (pair[0] == variable) {
      return pair[1]
    }
  }
  return false
}

function loadParams() {
  try {
    var user_id = this.getQueryVariable('user_id') || '' //获取user_id

    var user_name = this.getQueryVariable('user_name') || '' //user_name

    user_name = decodeURI(user_name) //转码将解码方式unscape换为decodeURI，将中文参数获取

    var token = this.getQueryVariable('token') || '' //token

    var access_token = this.getQueryVariable('access_token') || ''

    var json = {
      token: token,
      user_id: user_id,
      user_name: user_name,
      access_token: access_token,
    }
    return json
  } catch (err) {
    console.log(err)
    // alert('账号已过期请重新登入！')
    // window.location.href = "http://175.24.190.188:88/#/login"
  }
}
//unity 调用js方法开始试验，并将参数传递给unity
function SendSouces() {
  const json = loadParams()
  console.log(this.ins)
  gameInstance.SendMessage('Root', 'GetWebGLToken', JSON.stringify(json))
}

function Reset() {
  // canvas.height = document.documentElement.clientHeight;//获取body可见区域高度
  // canvas.width = document.documentElement.clientWidth;//获取body可见区域高度

  if (
    document.getElementById('fullScreenButton').style.backgroundImage ==
    'url("TemplateData/fullScreen_off.png")'
  ) {
    let h = document.documentElement.clientHeight
    let w = document.documentElement.clientWidth
    canvas.style.width = '100vw'
    canvas.style.height = '100vh'
  } else if (
    document.getElementById('fullScreenButton').style.backgroundImage ==
    'url("TemplateData/fullScreen_on.png")'
  ) {
    let h = document.documentElement.clientHeight
    let w = document.documentElement.clientWidth
    canvas.style.width = h * 1.7 + 'px'
    canvas.style.height = h + 'px'
    // canvas.style.width = '100vw'
    // canvas.style.height = '100vh'
  }
}
window.οnresize = function () {
  Reset()
}

function ToggleFullScreen() {
  var isInFullScreen =
    (document.fullscreenElement && document.fullscreenElement !== null) ||
    (document.webkitFullscreenElement &&
      document.webkitFullscreenElement !== null) ||
    (document.mozFullScreenElement && document.mozFullScreenElement !== null) ||
    (document.msFullscreenElement && document.msFullscreenElement !== null)
  var element = container

  if (!isInFullScreen) {
    document.getElementById('fullScreenButton').style.backgroundImage =
      "url('TemplateData/fullScreen_off.png')"
    return (
      element.requestFullscreen ||
      element.webkitRequestFullscreen ||
      element.mozRequestFullScreen ||
      element.msRequestFullscreen
    ).call(element)
  } else {
    document.getElementById('fullScreenButton').style.backgroundImage =
      "url('TemplateData/fullScreen_on.png')"
    if (document.exitFullscreen) {
      document.exitFullscreen()
    } else if (document.webkitExitFullscreen) {
      document.webkitExitFullscreen()
    } else if (document.mozCancelFullScreen) {
      document.mozCancelFullScreen()
    } else if (document.msExitFullscreen) {
      document.msExitFullscreen()
    }
  }
}
