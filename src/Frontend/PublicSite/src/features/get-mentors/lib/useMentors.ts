import { useState, useEffect } from 'react';
import { mentorApi } from '@/entities/mentor/api/mentor.api.js';
import type { MentorDto } from '@/entities/mentor/types/mentor.types.js';

export function useMentors(limit?: number) {
  const [mentors, setMentors] = useState<MentorDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadMentors() {
      try {
        const data = await mentorApi.getAll();
        setMentors(limit ? data.slice(0, limit) : data);
      } catch (err) {
        setError('Ошибка загрузки');
        console.error('Mentor loading error:', err);
      } finally {
        setLoading(false);
      }
    }

    loadMentors();
  }, [limit]);

  return { mentors, loading, error };
}
