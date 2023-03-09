using System;
using System.IO;

namespace SentimentAnalysis
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			// 파일 읽기, 문자열 토큰화
			// 900개 리뷰파일 읽기
			string[] text = new string[900];
			for (int i = 0; i < 900; i++)
			{
				text[i] = File.ReadAllText(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\negative_reviews\cv" + i + ".txt");
			}
			// 단어 세기


			// 리뷰에서 한 번도 나타나지 않은 단어 삭제 & 단어 카운트 저장 (파일 쓰기)


			// positive, negative 분류 알고리즘 구현 ( 입력: 리뷰 파일, 단어 리스트)


		}
	}
}
