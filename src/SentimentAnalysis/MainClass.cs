using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SentimentAnalysis
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			string fileLocation = "C:\\Users\\YongHo\\SentimentAnalysis\\첨부파일\\과제3\\";

			// list.txt 내용 저장
			string[] negativeFileList = new string[900];
			string[] positiveFileList = new string[900];

			int count = 0;
			foreach (string line in File.ReadLines(fileLocation + @"negative_file_list.txt"))
			{
				negativeFileList[count] = line;
				count++;
			}

			count = 0;
			foreach (string line in File.ReadLines(fileLocation + @"positive_file_list.txt"))
			{
				positiveFileList[count] = line;
				count++;
			}

			// 900개 리뷰파일 읽기 (파일 읽기)
			string[] negativeReviews = new string[900];
			string[] positiveReviews = new string[900];

			for (int i = 0; i < negativeReviews.Length; i++)
			{
				negativeReviews[i] = File.ReadAllText(fileLocation + @"negative_reviews\" + negativeFileList[i]);
				negativeReviews[i] = Regex.Replace(negativeReviews[i], @"\d", "");
			}

			for (int i = 0; i < positiveReviews.Length; i++)
			{
				positiveReviews[i] = File.ReadAllText(fileLocation + @"positive_reviews\" + positiveFileList[i]);
				positiveReviews[i] = Regex.Replace(positiveReviews[i], @"\d", "");
			}

			// 백과사전 불러오기(Map, Dictionary)
			Dictionary<string, int> negativeWords = new Dictionary<string, int>();
			string[] negativeWordsList = File.ReadAllLines(fileLocation + @"negative-words.txt");
			foreach (string word in negativeWordsList)
			{
				negativeWords.Add(word, 0);
			}

			Dictionary<string, int> positiveWords = new Dictionary<string, int>();
			string[] positiveWordsList = File.ReadAllLines(fileLocation + @"positive-words.txt");
			foreach (string word in positiveWordsList)
			{
				positiveWords.Add(word, 0);
			}

			// 부정 리뷰에 포함된 모든 단어 세기 (문자열 토큰화)
			for (int i = 0; i < negativeReviews.Length; i++)
			{
				string[] negativeReviewWords = negativeReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);

				// 백과사전에 있는 단어를 토대로 나만의 백과사전 만들기 and 단어 수 세기 (C# Map)
				for (int j = 0; j < negativeReviewWords.Length; j++)
				{
					if (negativeWords.ContainsKey(negativeReviewWords[j]))
					{
						negativeWords[negativeReviewWords[j]]++;
					}
				}
			}
			// value값이 0인 단어 삭제
			List<string> NegativeKeyList = new List<string>();

			foreach (KeyValuePair<string, int> item in negativeWords)
			{
				if (item.Value == 0)
				{
					NegativeKeyList.Add(item.Key);
				}
			}

			foreach (string key in NegativeKeyList)
			{
				negativeWords.Remove(key);
			}

			// 나만의 부정단어장(negativeWords) 출력
			foreach (KeyValuePair<string, int> item in negativeWords)
			{
				Console.WriteLine(item.Key + " " + item.Value);
			}

			// 긍정 리뷰에 포함된 모든 단어 세기 (문자열 토큰화)
			for (int i = 0; i < positiveReviews.Length; i++)
			{
				string[] positiveReviewWords = positiveReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);

				// 백과사전에 있는 단어를 토대로 나만의 백과사전 만들기 and 단어 수 세기 (C# Map)
				for (int j = 0; j < positiveReviewWords.Length; j++)
				{
					if (positiveWords.ContainsKey(positiveReviewWords[j]))
					{
						positiveWords[positiveReviewWords[j]]++;
					}
				}
			}

			// value값이 0인 단어 삭제
			List<string> PositiveKeyList = new List<string>();
			foreach (KeyValuePair<string, int> item in positiveWords)
			{
				if (item.Value == 0)
				{
					PositiveKeyList.Add(item.Key);
				}
			}
			foreach (string key in PositiveKeyList)
			{
				positiveWords.Remove(key);
			}

			// 나만의 긍정단어장(positiveWords) 출력
			foreach (KeyValuePair<string, int> item in positiveWords)
			{
				Console.WriteLine(item.Key + " " + item.Value);
			}

			// 나만의 부정, 긍정 단어장 텍스트 파일로 저장(negative-sentiment, positive-sentiment)
			string outputFilePath1 = fileLocation + "실제경과\\negative-sentiment.txt";
			string outputFilePath2 = fileLocation + "실제경과\\positive-sentiment.txt";

			using (StreamWriter writer = new StreamWriter(outputFilePath1))
			{
				foreach (KeyValuePair<string, int> item in negativeWords)
				{
					writer.WriteLine(item.Key + " " + item.Value);
				}
			}

			using (StreamWriter writer = new StreamWriter(outputFilePath2))
			{
				foreach (KeyValuePair<string, int> item in positiveWords)
				{
					writer.WriteLine(item.Key + " " + item.Value);
				}
			}

			// 나만의 백과사전을 기반으로 10개의 리뷰 파일을 positive, negative, don't know로 분류
			string[] tests = new string[10];
			string[] testsWords;
			int positiveCount = 0;
			int negativeCount = 0;
			string[] result = new string[10];

			for (int i = 1; i < 11; i++)
			{
				tests[i - 1] = File.ReadAllText(fileLocation + @"test\" + string.Format("{0:00}", i) + ".txt");
				testsWords = tests[i - 1].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);
				for (int j = 0; j < testsWords.Length; j++)
				{
					if (negativeWords.ContainsKey(testsWords[j]))
					{
						negativeCount++;
					}

					else if (positiveWords.ContainsKey(testsWords[j]))
					{
						positiveCount++;
					}
				}

				// positive, negative 분류 알고리즘
				if (negativeCount > positiveCount)
				{
					result[i - 1] = "negative";
				}
				else if (positiveCount > negativeCount)
				{
					result[i - 1] = "positive";
				}
				else
				{
					result[i - 1] = "don't know";
				}
				Console.WriteLine(result[i - 1]);
			}

			// 분류 결과를 result.txt에 저장
			File.WriteAllLinesAsync(fileLocation + "실제경과\\result.txt", result);
		}
	}
}
