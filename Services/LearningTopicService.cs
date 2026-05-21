using StudyTracker.Api.DTOs;
using StudyTracker.Api.Models;

namespace StudyTracker.Api.Services;

/// <summary>
/// Provides simple in-memory CRUD operations for learning topics.
/// </summary>
public class LearningTopicService
{
    /// <summary>
    /// Stores all topics in application memory for the lifetime of the app.
    /// </summary>
    private readonly List<LearningTopic> topics = new List<LearningTopic>
    {
        new LearningTopic(1, "ASP.NET Core Web API", "Learning"),
        new LearningTopic(2, "EF Core", "Planned"),
        new LearningTopic(3, "PostgreSQL", "Planned")
    };

    /// <summary>
    /// Returns all learning topics.
    /// </summary>
    /// <returns>The full in-memory list of topics.</returns>
    public List<LearningTopic> GetAll()
    {
        return topics;
    }

    /// <summary>
    /// Finds a topic by its identifier.
    /// </summary>
    /// <param name="Id">The topic identifier to search for.</param>
    /// <returns>The matching topic, or <see langword="null"/> when it is not found.</returns>
    public LearningTopic? GetById(int id)
    {
        LearningTopic? foundTopic = null;

        // Loop through all topics and keep the matching topic when the id is found.
        foreach (LearningTopic topic in topics)
        {
            if (topic.Id == id)
            {
                foundTopic = topic;
            }
        }

        return foundTopic;
    }

    /// <summary>
    /// Creates a new topic and adds it to the in-memory list.
    /// </summary>
    /// <param name="request">The data used to create the topic.</param>
    /// <returns>The newly created topic.</returns>
    public LearningTopic Create(CreateLearningTopicRequest request)
    {
        // The next id is generated from the current number of topics.
        int newId = topics.Count + 1;
        LearningTopic newTopic = new LearningTopic(newId, request.Title, request.Status);

        topics.Add(newTopic);

        return newTopic;
    }

    /// <summary>
    /// Updates an existing topic by replacing the old record in the list.
    /// </summary>
    /// <param name="id">The identifier of the topic to update.</param>
    /// <param name="request">The updated topic data.</param>
    /// <returns>The updated topic, or <see langword="null"/> when the topic does not exist.</returns>
    public LearningTopic? Update(int id, UpdateLearningTopicRequest request)
    {
        int foundIndex = -1;

        // Search for the index so the existing item can be replaced in-place.
        for (int i = 0; i < topics.Count; i++)
        {
            if (topics[i].Id == id)
            {
                foundIndex = i;
            }
        }

        if (foundIndex == -1)
        {
            return null;
        }

        // Replace the old topic with a new record containing the updated values.
        LearningTopic updatedTopic = new LearningTopic(id, request.Title, request.Status);
        topics[foundIndex] = updatedTopic;

        return updatedTopic;
    }

    /// <summary>
    /// Deletes a topic from the in-memory list.
    /// </summary>
    /// <param name="id">The identifier of the topic to delete.</param>
    /// <returns><see langword="true"/> when the topic was removed; otherwise, <see langword="false"/>.</returns>
    public bool Delete(int id)
    {
        int foundIndex = -1;

        // Search for the index of the topic that should be removed.
        for (int i = 0; i < topics.Count; i++)
        {
            if (topics[i].Id == id)
            {
                foundIndex = i;
            }
        }

        if (foundIndex == -1)
        {
            return false;
        }

        topics.RemoveAt(foundIndex);

        return true;
    }
}
