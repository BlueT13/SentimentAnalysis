using System;
using System.IO;

namespace SentimentAnalysis
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			// 900개 리뷰파일 읽기 (파일 읽기)
			string negativeFileList = File.ReadAllText(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\negative_file_list.txt");
			string positiveFileList = File.ReadAllText(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\positive_file_list.txt");


			// 리뷰에 포함된 단어 세기 (문자열 토큰화)


			// 리뷰에서 한 번도 나타나지 않은 단어 삭제 & 단어 카운트 저장 (파일 쓰기)


			// positive, negative 분류 알고리즘 구현 ( 입력: 리뷰 파일, 단어 리스트)

			// 10개의 리뷰 파일을 positive, negative, don't know로 분류

			// 결과를 result.txt에 저장
		}
	}
}
