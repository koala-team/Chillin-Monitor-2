# -*- coding: utf-8 -*-

# chillin imports
from koala_serializer import generate


all_args = [('python', '.', 'snake_case'),
            ('cs', '../Assets/Koala/Scripts/KS', 'PascalCase')]

for args in all_args:
    generate('./messages.ks', *args)
    generate('./scene_actions.ks', *args)
