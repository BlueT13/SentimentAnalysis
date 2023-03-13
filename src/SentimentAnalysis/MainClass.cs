using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SentimentAnalysis
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			// list.txt 내용 저장
			string[] negativeFileList = new string[900];
			string[] positiveFileList = new string[900];

			int count = 0;
			foreach (string line in File.ReadLines(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\negative_file_list.txt"))
			{
				negativeFileList[count] = line;
				count++;
			}

			count = 0;
			foreach (string line in File.ReadLines(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\positive_file_list.txt"))
			{
				positiveFileList[count] = line;
				count++;
			}

			// 900개 리뷰파일 읽기 (파일 읽기)
			string[] negativeReviews = new string[900];
			string[] positiveReviews = new string[900];

			for (int i = 0; i < negativeReviews.Length; i++)
			{
				negativeReviews[i] = File.ReadAllText(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\negative_reviews\" +
					negativeFileList[i]);
				negativeReviews[i] = Regex.Replace(negativeReviews[i], @"\d", "");
			}

			for (int i = 0; i < positiveReviews.Length; i++)
			{
				positiveReviews[i] = File.ReadAllText(@"C:\Users\YongHo\SentimentAnalysis\첨부파일\과제3\positive_reviews\" +
					positiveFileList[i]);
				positiveReviews[i] = Regex.Replace(positiveReviews[i], @"\d", "");
			}

			// 백과사전 불러오기(c# hash set)


			// 리뷰에 포함된 단어 세기 (문자열 토큰화)
			for (int i = 0; i < negativeReviews.Length; i++)
			{
				string[] negativeReviewWords = negativeReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"' },
					StringSplitOptions.RemoveEmptyEntries);

				// 백과사전에 있는 단어를 토대로 나만의 백과사전 만들기 and 단어 수 세기 (C# Map)
				for (int j = 0; j < negativeReviewWords.Length; j++)
				{

				}
			}

			for (int i = 0; i < positiveReviews.Length; i++)
			{
				string[] positiveReviewWords = negativeReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"' },
					StringSplitOptions.RemoveEmptyEntries);

				// 백과사전에 있는 단어를 토대로 나만의 백과사전 만들기 and 단어 수 세기 (C# Map)
				for (int j = 0; j < positiveReviewWords.Length; j++)
				{

				}
			}

			// positive, negative 분류 알고리즘 구현 ( 입력: 리뷰 파일, 단어 리스트)

			// 10개의 리뷰 파일을 positive, negative, don't know로 분류

			// 결과를 result.txt에 저장
		}
	}
}
