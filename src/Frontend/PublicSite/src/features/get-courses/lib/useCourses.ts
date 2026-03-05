import { useState, useEffect } from 'react';
import { courseApi } from '@/entities/course/api/course.api.js';
import type { CourseDto } from '@/entities/course/types/course.types.js';

export function useCourses(limit?: number) {
  const [courses, setCourses] = useState<CourseDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadCourses() {
      try {
        const data = await courseApi.getAll();
        setCourses(limit ? data.slice(0, limit) : data);
      } catch (err) {
        setError('Ошибка загрузки курсов');
        console.error('Course loading error:', err);
      } finally {
        setLoading(false);
      }
    }

    loadCourses();
  }, [limit]);

  return { courses, loading, error };
}
