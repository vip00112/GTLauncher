using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public enum PacketType
    {
        JoinFail, // 서버 입장 실패
        JoinSuccess, // 서버 입장 성공
        Exit, // 서버퇴장

        Connected, // 다른 유저의 접속
        Disconnected, // 다른 유저의 종료

        Text, // 텍스트

        CallVoiceChat, // 음성채팅 신청
        AnswerVoiceChat, // 음성채팅 수락

        Audio, // 음성정보
        File, // 파일전송
    }
}
