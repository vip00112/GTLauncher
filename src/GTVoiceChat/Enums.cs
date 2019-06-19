using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public enum PacketType
    {
        LoginResult, // 서버 입장 결과
        Exit, // 서버퇴장
        Connected, // 다른 유저의 접속
        Disconnected, // 다른 유저의 종료
        Audio, // 음성정보
    }
}
